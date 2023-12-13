using System.Security.Claims;
using JsonWebTokenExample.WebAPI.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JsonWebTokenExample.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthenticationService _authenticationService;
        public AuthController(ILogger<AuthController> logger,
                              IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> IssueToken(string userName)
        {
            return Ok(await _authenticationService.IssueTokenAsync(userName));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> TestAuth()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            //middleware would throw a 401 if no Authorization header is provided, so authorizationHeaders will always have at least one value
            return Ok(User.FindFirstValue("name"));
        }
    }
}