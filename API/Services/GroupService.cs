using API.Exceptions;
using API.Models.Activities;
using DAL.UnitOfWork;
using DAL;
using API.Models.Groups;
using DAL.DataEntities;
using Microsoft.IdentityModel.Tokens;
using API.ViewModels;

namespace API.Services
{
    public class GroupService
    {
        private readonly IUnitOfWork _uow;

        public GroupService()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        public async Task<CreateGroupResult> CreateGroup(CreateGroupRequest request)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Group name can't be empty");

            var users = new List<User>();

            if (request.UserGuids.Any())
            {
                foreach (var userGuid in request.UserGuids)
                {
                    var user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == userGuid);

                    if (user != null)
                    {
                        users.Add(user);
                    }
                }
            }

            if (!users.Any())
                throw new OperationException(StatusCodes.Status400BadRequest, "Can't create group without any user.");

            var group = new Group
            {
                Name = request.Name,
                Description = request.Description,
                Users = users
            };

            await _uow.GroupRepo.AddAsync(group);
            await _uow.CompleteAsync();

            return new CreateGroupResult("Group has been created succesfully!");
        }

        public async Task<GetGroupResult> GetGroup(int groupId)
        {
            var group = await _uow.GroupRepo.FindAsync(groupId);

            return new GetGroupResult
            {
                Name = group.Name,
                Description = group.Description,
                Users = group.Users.Select(
                    u => new UserViewModel
                    {
                        Name = u.Name,
                        LastName = u.LastName
                    })
            };
        }

        public async Task<IEnumerable<GetGroupResult>> GetGroups(string userId)
        {
            var groups = await _uow.GroupRepo.WhereAsync(g => g.Users.Any( u => u.Id == Guid.Parse(userId)));

            return groups.Select(g => g.ToGetActivitiesResult());
        }

        public async Task<EditGroupResult> EditGroup(EditGroupRequest request)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Group name can't be empty");

            var group = await _uow.GroupRepo.FindAsync(request.Id);

            if (group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no activity like this");

            var users = new List<User>();

            if (request.UserGuids.Any())
            {
                foreach (var userGuid in request.UserGuids)
                {
                    var user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == userGuid);

                    if (user != null)
                    {
                        users.Add(user);
                    }
                }
            }

            if (!users.Any())
                throw new OperationException(StatusCodes.Status400BadRequest, "Can't create group without any user.");

            group.Name = request.Name;
            group.Description = request.Description;
            group.Users = users;

            _uow.GroupRepo.Update(group);
            await _uow.CompleteAsync();

            return new EditGroupResult("Group has been edited successfully!");
        }

        public async Task<DeleteGroupResult> DeleteGroup(int groupId)
        {
            var group = await _uow.GroupRepo.FindAsync(groupId);

            if (group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no group like this");

            _uow.GroupRepo.Remove(group);
            await _uow.CompleteAsync();

            return new DeleteGroupResult("Group has been deleted successfully!");
        }
    }
}

public static class GroupsServiceExtensions
{
    public static GetGroupResult ToGetActivitiesResult(this Group group)
    {
        return new GetGroupResult
        {
            Name = group.Name,
            Description = group.Description,
            Users = group.Users.Select(
                    u => new UserViewModel
                    {
                        Name = u.Name,
                        LastName = u.LastName
                    })
        };
    }
}

