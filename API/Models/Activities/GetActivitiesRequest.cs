namespace API.Models.Activities
{
    public class GetActivitiesRequest
    {
        public bool IsPrivate { get; set; }
        public string? UserGuid { get; set; }
        public string? GroupGuid { get; set; }
    }
}
