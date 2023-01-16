using API.Models.Activities;
using API.Models.Groups;
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
        [ProducesResponseType(typeof(CreateGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
        {
            var result = await _groupService.CreateGroup(request);

            return Ok(result);
        }

        [HttpGet("get/{groupId:int}")]
        [ProducesResponseType(typeof(GetGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var result = await _groupService.GetGroup(groupId);

            return Ok(result);
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(IEnumerable<GetGroupResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroups()
        {
            string userId = HttpContext.User.FindFirstValue("id");
            //var result = await _groupService.GetGroups(userId);

            //return Ok(result);
            return Ok();
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(EditGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditGroup(EditGroupRequest request)
        {
            var result = await _groupService.EditGroup(request);

            return Ok(result);
        }

        [HttpDelete("delete/{groupId:int}")]
        [ProducesResponseType(typeof(DeleteGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            var result = await _groupService.DeleteGroup(groupId);

            return Ok(result);
        }
    }
}
