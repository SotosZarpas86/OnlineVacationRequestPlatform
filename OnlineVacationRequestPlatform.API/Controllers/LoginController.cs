using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using OnlineVacationRequestPlatform.BusinessLayer.Models;
using System;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginEmployeeAsync([FromBody] LoginModel login)
        {
            try
            {
                var result = await _userService.AuthenticateUserAsync(login.Email, login.Password, "Employee");
                if (result.Id != Guid.Empty)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAdminAsync([FromBody] LoginModel login)
        {
            try
            {
                var result = await _userService.AuthenticateUserAsync(login.Email, login.Password, "Admin");
                if (result.Id != Guid.Empty)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
