using DAL.CommonVariables;

namespace API.Models.ToDoLists
{
    public class SetTemplateRequest
    {
        public string ToDoListGuid { get; set; }

        public ToDoListType ToDoListType { get; set; }
    }
}
