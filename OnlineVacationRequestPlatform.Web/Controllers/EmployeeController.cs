using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVacationRequestPlatform.Web.Models;
using OnlineVacationRequestPlatform.Web.Services;
using OnlineVacationRequestPlatform.Web.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly VacationRequestService _vacationRequestService;
        private readonly LoginService _loginService;
        private readonly IMailService _mailService;
        private readonly UserService _userService;

        public EmployeeController(VacationRequestService vacationRequestService,
                                  LoginService loginService,
                                  IMailService mailService,
                                  UserService userService)
        {
            _vacationRequestService = vacationRequestService;
            _loginService = loginService;
            _mailService = mailService;
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            var result = await _loginService.LoginEmployeeAsync(login);
            if (result.Id != Guid.Empty)
            {
                HttpContext.Session.SetString("UserId", result.Id.ToString());
                HttpContext.Session.SetString("Email", result.Email);
                HttpContext.Session.SetString("Role", result.RoleName);
                if (result.SupervisorId.HasValue)
                    HttpContext.Session.SetString("SupervisorId", result.SupervisorId.Value.ToString());

                return RedirectToAction("Index", "Employee");
            }
            else
                return View(login);
        }

        public async Task<ActionResult> IndexAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var roleName = HttpContext.Session.GetString("Role");
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Employee");
            if (!string.IsNullOrWhiteSpace(roleName) && roleName.Equals("Admin"))
                return RedirectToAction("Login", "Admin");

            var result = await _vacationRequestService.GetVacationRequestsByUserAsync(Guid.Parse(userId));
            return View(result.OrderByDescending(r => r.DateSubmitted));
        }

        public ActionResult Create()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var roleName = HttpContext.Session.GetString("Role");
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Employee");
            if (!string.IsNullOrWhiteSpace(roleName) && roleName.Equals("Admin"))
                return RedirectToAction("Login", "Admin");

            return View("New");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(VacationRequestViewModel vacationRequest)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                var email = HttpContext.Session.GetString("Email");
                var supervisorId = HttpContext.Session.GetString("SupervisorId");
                var supervisorEmail = string.Empty;

                //If there is no supervisor available, take the first admin to approve the request               
                if (!string.IsNullOrWhiteSpace(supervisorId))
                {
                    var user = await _userService.GetUserByIdAsync(Guid.Parse(supervisorId));
                    if (user.Id != Guid.Empty)
                        supervisorEmail = user.Email;
                    else
                    {
                        var availableUsers = await _userService.GetUserListAsync();
                        supervisorEmail = availableUsers.Where(u => u.RoleName.Equals("Admin")).FirstOrDefault().Email;                       
                    }
                }
                else
                {
                    var availableUsers = await _userService.GetUserListAsync();
                    supervisorEmail = availableUsers.Where(u => u.RoleName.Equals("Admin")).FirstOrDefault().Email;
                }

                var request = new VacationApplicationViewModel
                {
                    VacationStartDate = vacationRequest.DateFrom,
                    VacationEndDate = vacationRequest.DateTo,
                    Reason = vacationRequest.Reason,
                    RequestStatus = RequestStatus.Pending,
                    DateSubmitted = DateTime.Now,
                    UserId = userId
                };
                request.DaysRequested = (int)(request.VacationEndDate - request.VacationStartDate).TotalDays;

                var result = await _vacationRequestService.SaveVacationRequestAsync(request);
                if (result.Id != Guid.Empty)
                {
                    var url = Request.Scheme + "://" + Request.Host.Value + "/Application/Validate?id=" + result.Id;
                    var emailContent = $"<html><body><p>Dear supervisor, <br><br>Employee User requested for some time off, starting on {vacationRequest.DateFrom:dd-MM-yyyy} and ending on {vacationRequest.DateTo:dd-MM-yyyy}, stating the reason: {vacationRequest.Reason}<br><br>Click the below link to approve or reject the application<br>If link is not clickable, copy and paste it in a browser tab<br><br><a href=''{url}''>{url}</a></p></body></html>";
                    await _mailService.SendEmailAsync(supervisorEmail, "Vacation Request", emailContent);
                }
                return RedirectToAction("Index", "Employee");
            }
            catch
            {
                return View("New", vacationRequest);
            }
        }
    }
}
