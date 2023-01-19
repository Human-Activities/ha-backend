using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/TodoLists")]
    [ApiController]
    public class ToDoListsController : Controller
    {
        private readonly ToDoListsService _toDoListService;

        public ToDoListsController(ToDoListsService toDoListService)
        {
            _toDoListService = toDoListService;
        }
    }
}
