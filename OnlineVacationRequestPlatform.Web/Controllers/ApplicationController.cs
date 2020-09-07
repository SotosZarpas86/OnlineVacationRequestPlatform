using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVacationRequestPlatform.Web.Models;
using OnlineVacationRequestPlatform.Web.Services;
using OnlineVacationRequestPlatform.Web.Utilities;
using System;
using System.Collections.Generic;
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
                var user = "ceiddust@gmail.com"; //TODO call user service
                var emailContent = $"<html><body><p>Dear employee, <br><br>Your application has been accepted.<br><br>Your application submitted on {vacationApplicationStatus.DateSubmitted.ToLocalTime()}</p></body></html>";
                await _mailService.SendEmailAsync(user, "RE: Vacation Request", emailContent);
                return RedirectToAction("Validate","Application", new { id = vacationApplicationStatus.VacationApplicationId });
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
                var user = "ceiddust@gmail.com"; //TODO call user service
                var emailContent = $"<html><body><p>Dear employee, <br><br>Your application has been rejected.<br><br>Your application submitted on {vacationApplicationStatus.DateSubmitted.ToLocalTime()}</p></body></html>";
                await _mailService.SendEmailAsync(user, "RE: Vacation Request", emailContent);
                return RedirectToAction("Validate", "Application", new { id = vacationApplicationStatus.VacationApplicationId });
            }
            else
                return View("Error");
        }
    }
}
