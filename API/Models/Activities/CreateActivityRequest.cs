namespace API.Models.Activities
{
    public class CreateActivityRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
