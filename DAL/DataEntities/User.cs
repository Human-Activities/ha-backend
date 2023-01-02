namespace DAL.DataEntities
{
    public class User
    {
        public User()
        {
            Calendars = new HashSet<Calendar>();
        }

        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
    }
}
