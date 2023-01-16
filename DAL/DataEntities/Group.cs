namespace DAL.DataEntities
{
    public class Group
    {
        public Group()
        {
            UserGroups = new HashSet<UserGroups>();
        }

        public int Id { get; set; }

        public Guid GroupGuid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public virtual ICollection<UserGroups> UserGroups { get; set; }
    }
}
