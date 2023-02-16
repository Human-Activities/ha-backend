using API.Models.Sections;
using DAL.CommonVariables;

namespace API.Models.ToDoLists
{
    public class EditToDoListRequest
    {
        public string ToDoListGuid { get; set; }

        public string Name { get; set; }

        public ToDoListType ToDoListType { get; set; }

        public bool IsFavourite { get; set; }

        public string? Description { get; set; }

        public ICollection<GetSectionResult>? Sections { get; set; }
    }
}
