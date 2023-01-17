namespace API.Models.Groups
{
    public class EditGroupRequest : CreateGroupRequest
    {
        public string GroupGuid { get; set; }
    }
}
