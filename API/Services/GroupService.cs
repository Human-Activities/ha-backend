using API.Exceptions;
using API.Models.Activities;
using DAL.UnitOfWork;
using DAL;
using API.Models.Groups;
using DAL.DataEntities;
using Microsoft.IdentityModel.Tokens;
using API.ViewModels;
using System.Runtime.InteropServices;

namespace API.Services
{
    public class GroupService
    {
        private readonly IUnitOfWork _uow;

        public GroupService()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        public async Task<GetGroupResult> CreateGroup(CreateGroupRequest request, int userId)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Group name can't be empty");

            var group = new Group
            {
                Name = request.Name,
                Description = request.Description
            };

            try
            {
                await _uow.GroupRepo.AddAsync(group);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            try
            {
                await _uow.UserGroupRepo.AddAsync(new UserGroups
                {
                    UserId = userId,
                    GroupId = group.Id
                });
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return group.ToGetGroupResult();
        }

        public async Task<GetGroupResult> GetGroup(string groupGuid)
        {
            var group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == Guid.Parse(groupGuid));

            if (group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no group like this");

            return group.ToGetGroupResult();
        }

        public async Task<IEnumerable<GetGroupResult>> GetGroups(int userId)
        {
            var userGroups = (await _uow.UserGroupRepo.WhereAsync(ug => ug.UserId == userId));

            if (userGroups == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no group like this");

            var groups = userGroups.Select(ug => ug.Group);

            return groups.Select(g => g.ToGetGroupResult());
        }

        public async Task<GetGroupResult> EditGroup(EditGroupRequest request)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Group name can't be empty");

            if (!Guid.TryParse(request.GroupGuid, out Guid groupGuid))
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. Group guid is incorrect");

            var group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == groupGuid);

            if (group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no group like this");

            group.Name = request.Name;
            group.Description = request.Description;

            try
            {
                _uow.GroupRepo.Update(group);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return group.ToGetGroupResult();
        }

        public async Task<DeleteGroupResult> DeleteGroup(string groupGuid)
        {
            var group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == Guid.Parse(groupGuid));

            if (group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no group like this");

            try
            {
                _uow.GroupRepo.Remove(group);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return new DeleteGroupResult("Group has been deleted successfully!");
        }
    }
}

public static class GroupsServiceExtensions
{
    public static GetGroupResult ToGetGroupResult(this Group group)
    {
        return new GetGroupResult
        {
            GroupGuid = group.GroupGuid.ToString(),
            Name = group.Name,
            Description = group.Description,
            Users = group.UserGroups.Select(
                    u => new UserViewModel
                    {
                        Login = u.User.Login,
                        Name = u.User.Name,
                        LastName = u.User.LastName
                    })
        };
    }
}

