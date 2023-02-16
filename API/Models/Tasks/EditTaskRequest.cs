namespace API.Models.Tasks
{
    public class EditTaskRequest : CreateTaskRequest
    {
        public string? TaskGuid { get; set; }
    }
}
