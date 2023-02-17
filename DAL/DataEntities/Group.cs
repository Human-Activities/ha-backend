namespace DAL.DataEntities
{
    public class Group
    {
        public Group()
        {
            Activities = new HashSet<Activity>();
            Bills = new HashSet<Bill>();
            ToDoLists = new HashSet<ToDoList>();
            UserGroups = new HashSet<UserGroups>();
        }

        public int Id { get; set; }

        public Guid GroupGuid { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Link { get; set; }


        public virtual ICollection<Activity> Activities { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }

        public virtual ICollection<ToDoList> ToDoLists { get; set; }

        public virtual ICollection<UserGroups> UserGroups { get; set; }
    }
}
