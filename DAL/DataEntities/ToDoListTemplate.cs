using DAL.CommonVariables;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataEntities
{
    public class ToDoListTemplate
    {
        public ToDoListTemplate()
        {
            Sections = new HashSet<Section>();
        }

        public int Id { get; set; }

        public Guid ToDoListTemplateGuid { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDateTime { get; set; }

        public string Name { get; set; }

        public ToDoListType ToDoListType { get; set; }

        public string? Description { get; set; }


        public virtual User User { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
