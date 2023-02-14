using DAL.CommonVariables;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataEntities
{
    public class ToDoList
    {
        public ToDoList()
        {
            Sections = new HashSet<Section>();
        }

        public int Id { get; set; }

        public Guid ToDoListGuid { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public int? GroupId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }

        public ToDoListType ToDoListType { get; set; }

        public bool IsFavourite { get; set; }

        public string? Description { get; set; }


        public virtual User? User { get; set; }

        public virtual Group? Group { get; set; }

        public virtual ICollection<Section>? Sections { get; set; }
    }
}