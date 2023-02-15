using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataEntities
{
    public class Activity
    {
        public int Id { get; set; }

        public Guid ActivityGuid { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int? GroupId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Description { get; set; }

        public bool IsPublic { get; set; } = false;


        public virtual User User { get; set; }

        public virtual Group? Group { get; set; } 

        public virtual Category Category { get; set; }
    }
}
