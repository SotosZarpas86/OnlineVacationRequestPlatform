using Microsoft.AspNetCore.Mvc;
using OnlineVacationRequestPlatform.Web.Models;
using OnlineVacationRequestPlatform.Web.Services;
using OnlineVacationRequestPlatform.Web.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly VacationRequestService _vacationRequestService;
        private readonly IMailService _mailService;
        private readonly UserService _userService;

        public ApplicationController(VacationRequestService vacationRequestService,
                                     IMailService mailService,
                                     UserService userService)
        {
            _vacationRequestService = vacationRequestService;
            _mailService = mailService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> ValidateAsync(Guid id)
        {
            var result = await _vacationRequestService.GetVacationRequestAsync(id);
            if (result == null)
                return View("Error");

            var vacationApplicationStatus = new VacationApplicationStatusViewModel
            {
                RequestStatus = result.RequestStatus,
                DateSubmitted = result.DateSubmitted,
                VacationApplicationId = result.Id,
                ApplicantId = result.UserId
            };
            return View(vacationApplicationStatus);
        }

        [HttpPost]
        public async Task<ActionResult> ApproveAsync(VacationApplicationStatusViewModel vacationApplicationStatus)
        {
            vacationApplicationStatus.RequestStatus = RequestStatus.Approved;
            var result = await _vacationRequestService.UpdateVacationRequestStatusAsync(vacationApplicationStatus);
            if (result)
            {
                var user = await GetSupervisorEmailAsync(vacationApplicationStatus.VacationApplicationId);
                var emailContent = $"<html><body><p>Dear employee, <br><br>Your application has been accepted.<br><br>Your application submitted on {vacationApplicationStatus.DateSubmitted.ToLocalTime()}</p></body></html>";
                await _mailService.SendEmailAsync(user, "RE: Vacation Request", emailContent);
                return RedirectToAction("Validate", "Application", new { id = vacationApplicationStatus.VacationApplicationId });
            }
            else
                return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> RejectAsync(VacationApplicationStatusViewModel vacationApplicationStatus)
        {
            vacationApplicationStatus.RequestStatus = RequestStatus.Rejected;
            var result = await _vacationRequestService.UpdateVacationRequestStatusAsync(vacationApplicationStatus);
            if (result)
            {
                var user = await GetSupervisorEmailAsync(vacationApplicationStatus.VacationApplicationId);
                var emailContent = $"<html><body><p>Dear employee, <br><br>Your application has been rejected.<br><br>Your application submitted on {vacationApplicationStatus.DateSubmitted.ToLocalTime()}</p></body></html>";
                await _mailService.SendEmailAsync(user, "RE: Vacation Request", emailContent);
                return RedirectToAction("Validate", "Application", new { id = vacationApplicationStatus.VacationApplicationId });
            }
            else
                return View("Error");
        }

        private async Task<string> GetSupervisorEmailAsync(Guid employeeId)
        {
            var supervisorEmail = string.Empty;
            var employee = await _userService.GetUserByIdAsync(employeeId);
            if(employee.Id != Guid.Empty)
            {
                //If there is no supervisor available, take the first admin to approve the request               
                if (employee.SupervisorId.HasValue)
                {
                    var user = await _userService.GetUserByIdAsync(employee.SupervisorId.Value);
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
            }
            return supervisorEmail;
        }
    }
}
