using API.Models.Activities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivityService _activityService;

        public ActivitiesController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(CreateActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateActivity(CreateActivityRequest request)
        {
            var result = await _activityService.CreateActivity(request);

            return Ok(result);
        }

        [HttpGet("get/{activityGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(GetActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActvity(string activityGuid)
        {
            var result = await _activityService.GetActivity(activityGuid);

            return Ok(result);
        }

        [HttpGet("get/{userGuid}.{groupGuid?}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(IEnumerable<GetActivityResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActvities(string userGuid, string? groupGuid = null)
        {
            var result = await _activityService.GetActivities(userGuid, groupGuid);

            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(EditActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditActivity(EditActivityRequest request)
        {
            var result = await _activityService.EditActivity(request);

            return Ok(result);
        }

        [HttpDelete("delete/{activityGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteActivity(string activityGuid)
        {
            await _activityService.DeleteActivity(activityGuid);

            return Ok();
        }
    }
}
