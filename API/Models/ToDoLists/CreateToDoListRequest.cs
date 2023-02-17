using API.Models.Sections;
using DAL.CommonVariables;

namespace API.Models.ToDoLists
{
    public class CreateToDoListRequest
    {
        public string? GroupGuid { get; set; }

        public string Name { get; set; }

        public ToDoListType ToDoListType { get; set; }

        public bool IsFavourite { get; set; }

        public string? Description { get; set; }

        public ICollection<CreateSectionRequest>? Sections { get; set; }
    }
}
