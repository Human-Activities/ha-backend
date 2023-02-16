using DAL.DataEntities;

namespace API.Models.Groups
{
    public class CreateGroupRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
