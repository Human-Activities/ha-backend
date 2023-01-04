namespace DAL.DataEntities
{
    public class ToDoListTemplate
    {
        public ToDoListTemplate()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
