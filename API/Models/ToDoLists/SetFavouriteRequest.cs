namespace API.Models.ToDoLists
{
    public class SetFavouriteRequest
    {
        public string ToDoListGuid { get; set; }

        public bool IsFavourite { get; set; }
    }
}
