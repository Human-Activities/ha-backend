using DAL.CommonVariables;

namespace DAL.DataEntities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Day Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set;}
        public int CalendarId { get; set; }

        public virtual Calendar Calendar { get; set; }
    }
}
