namespace DAL.DataEntities
{
    public class User
    {
        public User()
        {
            Activities= new HashSet<Activity>();
            Bills = new HashSet<Bill>();
            BillItems = new HashSet<BillItem>();
            ToDoLists = new HashSet<ToDoList>();
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

        public virtual ICollection<Activity> Activities{ get; set; }

        public virtual ICollection<Bill> Bills { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }

        public virtual ICollection<ToDoList> ToDoLists{ get; set; }

        public virtual ICollection<UserGroups> UserGroups { get; set; }
    }
}
