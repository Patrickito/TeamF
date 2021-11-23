using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using TeamF_Api.DTO;
using TeamF_Api.Security;
using TeamF_Api.Services.Exceptions;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService service, ILogger<AuthenticationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticationDto newUser)
        {
            _logger.LogDebug($"Registration request for user: ${newUser.UserName}");

            try
            {
                await _service.RegisterUser(newUser.UserName, newUser.Password);
            }
            catch (ConflictingUserNameException e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticationDto userData)
        {
            string token;
            try
            {
                token = await _service.Login(userData.UserName, userData.Password);
            }
            catch (AuthenticationException e)
            {
                _logger.LogError(e.Message);
                return Unauthorized();
            }

            var result = new { Token = token };

            return Ok(result);
        }

        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeDTO param)
        {
            string userName = GetUserName();
            try
            {
                await _service.ChangePassword(userName, param.OldPassword, param.NewPassword);
            }
            catch (AuthenticationException e)
            {
                _logger.LogError(e.Message);
                return Unauthorized();
            }

            return NoContent();
        }

        [Authorize(Policy = SecurityConstants.AdminPolicy)]
        [HttpPost("changeRoles")]
        public async Task<IActionResult> ChangeRoles([FromBody] RoleChangeDTO param)
        {
            try
            {
                await _service.UpdateRoles(param.UserName, param.Roles);
            }
            catch (AuthenticationException e)
            {
                _logger.LogError(e.Message);
                return Unauthorized();
            }

            return NoContent();
        }
    }
}
