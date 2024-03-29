﻿using API.Models.Activities;
using API.Models.Groups;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : Controller
    {
        private readonly GroupService _groupService;

        public GroupsController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(GetGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _groupService.CreateGroup(request, userId);

            return Ok(result);
        }

        [HttpGet("get/{groupGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(GetGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroup(string groupGuid)
        {
            var result = await _groupService.GetGroup(groupGuid);

            return Ok(result);
        }

        [HttpGet("get")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(IEnumerable<GetGroupResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroups()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _groupService.GetGroups(userId);

            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(GetGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditGroup(EditGroupRequest request)
        {
            var result = await _groupService.EditGroup(request);

            return Ok(result);
        }

        [HttpDelete("delete/{groupGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(DeleteGroupResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGroup(string groupGuid)
        {
            var result = await _groupService.DeleteGroup(groupGuid);

            return Ok(result);
        }
    }
}
