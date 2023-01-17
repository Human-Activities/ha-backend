using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Services;
using API.Models.Authentication;

namespace API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authenticationService.Register(request);

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authenticationService.Login(request);

            return Ok(result);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(RefreshResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _authenticationService.Refresh(request, userId);
            return Ok(result);
        }

        [HttpDelete("logout")]
        [ProducesResponseType(typeof(LogoutResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _authenticationService.Logout(userId);
            return Ok(result);
        }
    }
}
