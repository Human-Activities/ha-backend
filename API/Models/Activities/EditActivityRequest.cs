namespace API.Models.Activities
{
    public class EditActivityRequest : CreateActivityRequest
    {
        public string ActivityGuid { get; set; }
    }
}
