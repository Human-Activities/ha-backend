using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataEntities
{
    public class Calendar
    {
        public Calendar()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }

        public Guid CalendarGuid { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Name { get; set; }


        public virtual User User { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
