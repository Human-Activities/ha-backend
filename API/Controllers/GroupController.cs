using API.Models.Activities;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateGroup(CreateActivityRequest request)
        {
            var result = await _groupService.CreateGroup(request);

            return Ok(result);
        }

        [HttpGet("get/{groupId:int}")]
        [ProducesResponseType(typeof(GetActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var result = await _groupService.GetGroup(groupId);

            return Ok(result);
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(GetActivitiesResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroups(GetActivitiesRequest request)
        {
            string userId = HttpContext.User.FindFirstValue("id");
            var result = await _groupService.GetGroups(request, userId);

            return Ok(result);
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(EditActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditGroup(EditActivityRequest request)
        {
            var result = await _groupService.EditGroup(request);

            return Ok(result);
        }

        [HttpDelete("delete/{groupId:int}")]
        [ProducesResponseType(typeof(DeleteActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            var result = await _groupService.DeleteGroup(groupId);

            return Ok(result);
        }
    }
}
