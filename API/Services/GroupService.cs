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

        public async Task<CreateGroupResult> CreateGroup(CreateGroupRequest request, int userId)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Group name can't be empty");

            var group = new Group
            {
                Name = request.Name,
                Description = request.Description
            };

            await _uow.GroupRepo.AddAsync(group);
            await _uow.CompleteAsync();

            await _uow.UserGroupRepo.AddAsync(new UserGroups
            {
                User = await _uow.UserRepo.FindAsync(userId),
                Group = group
            });

            await _uow.CompleteAsync();

            return new CreateGroupResult("Group has been created succesfully!");
        }

        public async Task<GetGroupResult> GetGroup(string groupGuid)
        {
            var group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == Guid.Parse(groupGuid));

            if (group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no group like this");

            return new GetGroupResult
            {
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

        public async Task<IEnumerable<GetGroupResult>> GetGroups(int userId)
        {
            var groups = (await _uow.UserGroupRepo.WhereAsync(ug => ug.UserId == userId)).Select(ug => ug.Group);

            return groups.Select(g => g.ToGetActivitiesResult());
        }

        public async Task<EditGroupResult> EditGroup(EditGroupRequest request)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "Group name can't be empty");

            var group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == Guid.Parse(request.GroupGuid));

            if (group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no group like this");

            group.Name = request.Name;
            group.Description = request.Description;

            _uow.GroupRepo.Update(group);
            await _uow.CompleteAsync();

            return new EditGroupResult("Group has been edited successfully!");
        }

        /// <summary>
        /// function for adding user to group, by using generated link
        /// </summary>
        /// <param name="link">param with Group guid and user id</param> 
        /// <returns>information that user has been added to group successfulyy</returns>
        public async Task<string> AddUserToGroup(string link)
        {
            return "Added user succesfully";
        }

        public async Task<DeleteGroupResult> DeleteGroup(string groupGuid)
        {
            var group = await _uow.GroupRepo.SingleOrDefaultAsync(g => g.GroupGuid == Guid.Parse(groupGuid));

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

