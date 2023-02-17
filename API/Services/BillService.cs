using API.Exceptions;
using DAL.UnitOfWork;
using DAL;
using API.Models.Bills;
using Microsoft.IdentityModel.Tokens;
using DAL.DataEntities;
using API.Models.Categories;
using API.Models;

namespace API.Services
{
    public class BillService
    {
        private readonly IUnitOfWork _uow;

        public BillService()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        public async Task<CreateBillResult> CreateBill(CreateBillRequest request, int userId)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Bill name can't be empty");

            Group? group = null;

            if (request.GroupGuid != null)
                if (Guid.TryParse(request.GroupGuid, out Guid toDoListGuidParsed))
                    group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == toDoListGuidParsed);

            var bills = await _uow.BillRepo.WhereAsync(b => b.UserId == userId);

            int newAccountBillnumber = 1;

            if (bills.Any())
                newAccountBillnumber = bills.ToList().Max(b => b.AccountBillNumber) + 1;

            var bill = new Bill
            {
                UserId = userId,
                GroupId = group?.Id,
                Name = request.Name,
                AccountBillNumber = newAccountBillnumber,
                BillItems = request.BillItems.Select(
                    bi => new BillItem
                    {
                        UserId = userId,
                        Name = bi.Name,
                        TotalValue = bi.TotalValue,
                        CategoryId = bi.BillItemCategory.Id
                    }).ToList()
            };

            if (bill.BillItems != null && bill.BillItems.Any())
                bill.TotalValue = bill.BillItems.Sum(b => b.TotalValue);

            try
            {
                await _uow.BillRepo.AddAsync(bill);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return (CreateBillResult)bill.ToCreateBillResult();
        }

        public async Task<GetBillResult> GetBill(string BillGuid)
        {
            var bill = await _uow.BillRepo.SingleOrDefaultAsync(a => a.BillGuid == Guid.Parse(BillGuid));

            if (bill == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no Bill like this");

            return (GetBillResult)bill.ToCreateBillResult();
        }

        public async Task<IEnumerable<GetBillResult>> GetBills(int userId, string? groupGuid)
        {
            var bills = new List<GetBillResult>();

            if (groupGuid.IsNullOrEmpty())
            {
                bills = (await _uow.BillRepo
                    .WhereAsync(b => b.UserId == userId))
                    .Select(td => (GetBillResult)td.ToCreateBillResult()).ToList();
            }
            else
            {
                if (Guid.TryParse(groupGuid, out Guid toDoListGuidParsed))
                {
                    var group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == toDoListGuidParsed);
                    if (group == null)
                        throw new OperationException(StatusCodes.Status400BadRequest, "Group guid is incorrect");

                    bills = (await _uow.BillRepo.WhereAsync(b => b.GroupId == group.Id)).Select(b => (GetBillResult)b.ToCreateBillResult()).ToList();
                }
                else
                    throw new OperationException(StatusCodes.Status400BadRequest, "Group guid is incorrect");
            }

            return bills;
        }

        public async Task<EditBillResult> EditBill(EditBillRequest request)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Bill name can't be empty");

            Bill? bill = null;

            if (Guid.TryParse(request.BillGuid, out Guid billGuid))
            {
                bill = await _uow.BillRepo.SingleOrDefaultAsync(a => a.BillGuid == billGuid);
            }
            else
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "Bill guid is incorrect");
            }

            if (bill == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no Bill like this");

            bill.Name = request.Name;
            bill.TotalValue = request.TotalValue;

            if (!Guid.TryParse(request.UserGuid, out Guid userGuid))
                throw new OperationException(StatusCodes.Status400BadRequest, "User guid is incorrect");

            var user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.UserGuid == userGuid);

            if (user == null)
                throw new OperationException(StatusCodes.Status400BadRequest, "User with this guid does not exist");

            if (request.BillItems != null)
            {
                if (bill.BillItems != null && bill.BillItems.Any())
                {
                    foreach (var billItem in bill.BillItems)
                    {
                        var updatedBillItem = request.BillItems.SingleOrDefault(bi => bi.BillItemGuid == billItem.BillItemGuid.ToString());
                        if (updatedBillItem != null)
                        {
                            billItem.CategoryId = updatedBillItem.BillItemCategory.Id;
                            billItem.Name = updatedBillItem.Name;
                            billItem.TotalValue = updatedBillItem.TotalValue;

                            request.BillItems.Remove(updatedBillItem);
                        }
                        else
                        {
                            bill.BillItems.Remove(billItem);
                        }
                    }

                    if (request.BillItems.Any())
                    {
                        if (bill.BillItems == null)
                            bill.BillItems = new List<BillItem>();

                        foreach (var newBillItem in request.BillItems)
                        {
                            bill.BillItems.Add(new BillItem
                            {
                                UserId = user.Id,
                                CategoryId = newBillItem.BillItemCategory.Id,
                                Name = newBillItem.Name,
                                TotalValue = newBillItem.TotalValue,
                            });
                        }
                    }
                }
                else
                {
                    if (bill.BillItems == null)
                        bill.BillItems = new List<BillItem>();

                    foreach (var newBillItem in request.BillItems)
                    {
                        bill.BillItems.Add(new BillItem
                        {
                            UserId = user.Id,
                            CategoryId = newBillItem.BillItemCategory.Id,
                            Name = newBillItem.Name,
                            TotalValue = newBillItem.TotalValue,
                        });
                    }
                }
            }
            else if (bill.BillItems != null && bill.BillItems.Any())
            {
                foreach (var billItemToDelete in bill.BillItems)
                {
                    bill.BillItems.Remove(billItemToDelete);
                }
            }

            if (bill.BillItems != null && bill.BillItems.Any())
                bill.TotalValue = bill.BillItems.Sum(b => b.TotalValue);

            try
            {
                _uow.BillRepo.Update(bill);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return (EditBillResult)bill.ToCreateBillResult();
        }

        public async Task<DeleteBillResult> DeleteBill(string billGuidAsString)
        {
            Bill? bill = null;

            if (Guid.TryParse(billGuidAsString, out Guid billGuid))
            {
                bill = await _uow.BillRepo.SingleOrDefaultAsync(a => a.BillGuid == billGuid);
            }
            else
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "Bill guid is incorrect");
            }

            if (bill == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no Bill like this");

            try
            {
                _uow.BillRepo.Remove(bill);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return new DeleteBillResult("Bill has been deleted successfully!");
        }
    }

    public static class BillServiceExtensions
    {
        public static CreateBillRequest ToCreateBillResult(this Bill bill)
        {
            return new CreateBillRequest
            {
                BillGuid = bill.BillGuid.ToString(),
                UserGuid = bill.User.UserGuid.ToString(),
                GroupGuid = bill.Group?.GroupGuid.ToString(),
                Name = bill.Name,
                TotalValue = bill.TotalValue,
                CreatedDate = bill.CreatedDate,
                AccountBillNumber = bill.AccountBillNumber,
                BillItems = bill.BillItems.Select(
                    b => new CreateBillItemRequest
                    {
                        BillItemGuid = b.BillItemGuid.ToString(),
                        Name = b.Name,
                        TotalValue = b.TotalValue,
                        BillItemCategory = new BillItemCategory
                        {
                            Id = b.CategoryId,
                            Name = b.Category.Name
                        },
                        Author = new Author
                        {
                            AuthorGuid = b.User.UserGuid.ToString(),
                            Name = b.User.Login
                        }
                    }).ToList()
            };
        }
    }
}