using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DAL.DataEntities
{
    public class Calendar
    {
        public Calendar()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty; // Do we need it?

        public virtual User User { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
