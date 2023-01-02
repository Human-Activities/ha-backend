namespace DAL.DataEntities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public bool IsDone { get; set; }
        public int ToDoListId { get; set; }

        public virtual ToDoListTemplate ToDoListTemplate { get; set; }
    }
}
