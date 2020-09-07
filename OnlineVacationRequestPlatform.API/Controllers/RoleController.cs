using Microsoft.AspNetCore.Mvc;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            try
            {
                var result = await _roleService.GetAllAsync();
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
