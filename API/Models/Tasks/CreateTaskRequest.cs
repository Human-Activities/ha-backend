using DAL.CommonVariables;

namespace API.Models.Tasks
{
    public class CreateTaskRequest
    {
        public string Name { get; set; }

        public string? Notes { get; set; }

        public bool IsDone { get; set; }

        public TaskPriority Priority { get; set; }
    }
}
