using API.Models.ToDoLists;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/todolists")]
    [ApiController]
    public class ToDoListsController : Controller
    {
        private readonly ToDoListsService _toDoListService;

        public ToDoListsController(ToDoListsService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(GetToDoListResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateToDoList(CreateToDoListRequest request)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _toDoListService.CreateToDoList(request, userId);

            return Ok(result);
        }

        [HttpGet("get/{toDoListGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(GetToDoListResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetToDoList(string toDoListGuid)
        {
            var result = await _toDoListService.GetToDoList(toDoListGuid);

            return Ok(result);
        }

        [HttpGet("get-all/{groupGuid?}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(IEnumerable<GetToDoListResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetToDoLists(string? groupGuid = null)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _toDoListService.GetToDoLists(userId, groupGuid);

            return Ok(result);
        }

        [HttpGet("get-templates/{groupGuid?}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(IEnumerable<GetToDoListResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetToDoListTemplates(string? groupGuid = null)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _toDoListService.GetToDoListTemplates(userId, groupGuid);

            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(GetToDoListResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditToDoList(EditToDoListRequest request)
        {
            var result = await _toDoListService.EditToDoList(request);

            return Ok(result);
        }

        [HttpPut("set-favourite")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(SetFavouriteResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetFavourite(SetFavouriteRequest request)
        {
            var result = await _toDoListService.SetFavourite(request);

            return Ok(result);
        }

        [HttpPut("set-template")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(SetTemplateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetTemplate(SetTemplateRequest request)
        {
            var result = await _toDoListService.SetTemplate(request);

            return Ok(result);
        }

        [HttpDelete("delete/{toDoListGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(DeleteToDoListResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteToDoList(string toDoListGuid)
        {
            var result = await _toDoListService.DeleteToDoList(toDoListGuid);

            return Ok(result);
        }
    }
}
