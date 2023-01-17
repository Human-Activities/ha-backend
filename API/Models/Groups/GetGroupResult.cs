using API.ViewModels;

namespace API.Models.Groups
{
    public class GetGroupResult : EditGroupRequest
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
