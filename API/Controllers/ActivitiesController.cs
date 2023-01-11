﻿using API.Models.Activities;
using API.Models.TransferObjects;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

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
        [ProducesResponseType(typeof(CreateActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateActivity(CreateActivityRequest request)
        {
            var result = await _activityService.CreateActivity(request);

            return Ok(result);
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(GetActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActvities(GetActivityRequest request)
        {
            var result = await _activityService.GetActivity(request);

            return Ok(result);
        }

        [HttpGet("getall")]
        [ProducesResponseType(typeof(GetActivitiesResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActvities(GetActivitiesRequest request)
        {
            string userId = HttpContext.User.FindFirstValue("id");
            var result = await _activityService.GetActivities(request, userId);

            return Ok(result);
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(EditActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditActivity(EditActivityRequest request)
        {
            var result = await _activityService.EditActivity(request);

            return Ok(result);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(DeleteActivityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteActivity(DeleteActivityRequest request)
        {
            var result = await _activityService.DeleteActivity(request);

            return Ok(result);
        }
    }
}