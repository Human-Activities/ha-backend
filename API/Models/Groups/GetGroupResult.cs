using API.ViewModels;

namespace API.Models.Groups
{
    public class GetGroupResult
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
