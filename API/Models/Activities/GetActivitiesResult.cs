using DAL.DataEntities;

namespace API.Models.Activities
{
    public class GetActivitiesResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsTemplate { get; set; }

        public bool IsPrivate { get; set; }

        public virtual ICollection<Category>? Categories { get; set; }
    }
}
