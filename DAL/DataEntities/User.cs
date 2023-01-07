namespace DAL.DataEntities
{
    public class User
    {
        public User()
        {
            Calendars = new HashSet<Calendar>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }

        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
    }
}
