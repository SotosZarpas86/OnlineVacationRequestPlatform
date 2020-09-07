using Microsoft.AspNetCore.Mvc;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using OnlineVacationRequestPlatform.BusinessLayer.Models;
using System;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationRequestController : ControllerBase
    {
        private readonly IVacationRequestService _vacationRequestService;

        public VacationRequestController(IVacationRequestService vacationRequestService)
        {
            _vacationRequestService = vacationRequestService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllVacationRequestsByUserAsync(Guid id)
        {
            try
            {
                var result = await _vacationRequestService.GetAllByUserAsync(id);
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
        public async Task<IActionResult> GetVacationRequestByIdAsync(Guid id)
        {
            try
            {
                var result = await _vacationRequestService.GetΒyIdAsync(id);
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
        public async Task<IActionResult> AddVacationRequestAsync([FromBody] VacationRequestModel vacationRequest)
        {
            try
            {
                var result = await _vacationRequestService.AddAsync(vacationRequest);
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateRequestStatus")]
        public async Task<IActionResult> UpdateVacationRequestStatusAsync(VacationRequestStatusModel vacationRequestStatus)
        {
            try
            {
                var result = await _vacationRequestService.UpdateStatusAsync(vacationRequestStatus.VacationApplicationId, vacationRequestStatus.RequestStatus);
                if (result)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
