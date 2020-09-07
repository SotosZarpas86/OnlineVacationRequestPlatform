using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVacationRequestPlatform.Web.Models;
using OnlineVacationRequestPlatform.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly UserService _userService;
        private readonly ICacheService _cacheService;
        private readonly LoginService _loginService;

        public AdministrationController(UserService userService, ICacheService cacheService, LoginService loginService)
        {
            _userService = userService;
            _cacheService = cacheService;
            _loginService = loginService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            var result = await _loginService.LoginAdminAsync(login);
            if (result.Id != Guid.Empty)
            {
                HttpContext.Session.SetString("UserId", result.Id.ToString());
                HttpContext.Session.SetString("Email", result.Email);
                HttpContext.Session.SetString("Role", result.RoleName);
                return RedirectToAction("Index", "Administration");
            }               
            else
                return View(login);
        }

        public async Task<IActionResult> IndexAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var roleName = HttpContext.Session.GetString("Role");
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Admin");
            if (!string.IsNullOrWhiteSpace(roleName) && roleName.Equals("Employee"))
                return RedirectToAction("Login", "Employee");

            var result = await _userService.GetUserListAsync();
            return View(result);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var roleName = HttpContext.Session.GetString("Role");
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Admin");
            if (!string.IsNullOrWhiteSpace(roleName) && roleName.Equals("Employee"))
                return RedirectToAction("Login", "Employee");

            var user = new ExtendedUserViewModel();
            user = await GetAvailableRolesAsync(user);
            user = await GetAvailableSupervisorsAsync(user);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ExtendedUserViewModel user)
        {
            var result = await _userService.AddUserAsync(user);
            return View(result);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var roleName = HttpContext.Session.GetString("Role");
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Admin");
            if (!string.IsNullOrWhiteSpace(roleName) && roleName.Equals("Employee"))
                return RedirectToAction("Login", "Employee");

            var result = await _userService.GetUserByIdAsync(id);
            result = await GetAvailableRolesAsync(result);
            result = await GetAvailableSupervisorsAsync(result);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(ExtendedUserViewModel user)
        {
            var result = await _userService.UpdateUserAsync(user);
            return View(result);
        }

        private async Task<ExtendedUserViewModel> GetAvailableRolesAsync(ExtendedUserViewModel user)
        {
            var availableRoles = await _cacheService.GetCachedRoleListAsync();
            if (availableRoles.Any())
            {
                var roleList = availableRoles.Select(r => new { r.Id, r.Name }).ToList();
                user.AvailableRoles = new SelectList(roleList, "Id", "Name");
            }
            return user;
        }

        private async Task<ExtendedUserViewModel> GetAvailableSupervisorsAsync(ExtendedUserViewModel user)
        {
            var availableSupervisors = await _userService.GetUserListAsync();
            if (availableSupervisors.Any())
            {
                var supervisorsList = availableSupervisors.Where(e => e.RoleName.Equals("Employee")).Select(r => new { r.Id, r.FullName}).ToList();
                user.AvailableSupervisors = new SelectList(supervisorsList, "Id", "FullName");
            }
            return user;
        }
    }
}
