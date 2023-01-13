namespace DAL.DataEntities
{
    public class Activity
    {
        public Activity()
        {
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsTemplate { get; set; } = false;
        public bool IsPrivate { get; set; } = true;

        public virtual User User { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
