using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DAL.DataEntities
{
    public class Section
    {
        public Section()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        public Guid SectionGuid { get; set; }

        public int ToDoListId { get; set; }


        public virtual ICollection<Task> Tasks { get; set; }

    }
}
