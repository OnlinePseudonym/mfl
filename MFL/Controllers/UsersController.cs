using System.Threading.Tasks;
using MFL.Services;
using Microsoft.AspNetCore.Mvc;
using MFL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace MFL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticationRequest user)
        {
            try
            {
                var response = await _userService.AuthenticateUser(user);

                if (response.IsAuthenticated)
                {
                    return Ok(response);
                }

                return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Validate([FromBody] string token)
        {
            try
            {
                var response = _userService.ValidateCurrentToken(token);

                if (response)
                {
                    return Ok(response);
                }

                return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
