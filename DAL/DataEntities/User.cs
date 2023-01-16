namespace DAL.DataEntities
{
    public class User
    {
        public User()
        {
            Calendars = new HashSet<Calendar>();
            Activities= new HashSet<Activity>();
            ToDoListTemplates = new HashSet<ToDoListTemplate>();
            UserCosts = new HashSet<UserCosts>();
            UserGroups = new HashSet<UserGroups>();
        }

        public int Id { get; set; }

        public Guid UserGuid { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public int RoleId { get; set; }


        public virtual UserRole UserRole { get; set; }

        public virtual ICollection<Calendar> Calendars { get; set; }

        public virtual ICollection<Activity> Activities{ get; set; }

        public virtual ICollection<ToDoListTemplate> ToDoListTemplates{ get; set; }

        public virtual ICollection<UserCosts> UserCosts { get; set; }

        public virtual ICollection<UserGroups> UserGroups { get; set; }
    }
}
