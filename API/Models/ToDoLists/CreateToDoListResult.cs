using API.Models.Activities;
using API.Models.ToDoLists;

namespace API.Models.ToDoLists
{
    public class CreateToDoListResult : CreateActivityResult
    {
        public CreateToDoListResult(string message) : base(message)
        {
        }
    }
}
