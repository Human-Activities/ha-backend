using API.Models.Categories;

namespace API.Models.Activities
{
    public class CreateActivityRequest
    {
        public string UserGuid { get; set; }
        public string? GroupGuid { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
        public ActivityCategory Category { get; set; }
    }
}
