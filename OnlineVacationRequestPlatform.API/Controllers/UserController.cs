using Microsoft.AspNetCore.Mvc;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using OnlineVacationRequestPlatform.BusinessLayer.Models;
using System;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var result = await _userService.GetExtendedAllAsync();
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            try
            {
                var result = await _userService.GetΒyIdAsync(id);
                if (result != null)
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
        [Route("Create")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserModel user)
        {
            try
            {
                var result = await _userService.AddAsync(user);
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserModel user)
        {
            try
            {
                var result = await _userService.UpdateAsync(user);
                if (result != null)
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
