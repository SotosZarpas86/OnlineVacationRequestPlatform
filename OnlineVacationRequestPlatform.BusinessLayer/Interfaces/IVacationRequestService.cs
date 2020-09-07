using OnlineVacationRequestPlatform.BusinessLayer.Models;
using OnlineVacationRequestPlatform.BusinessLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.BusinessLayer.Interfaces
{
    public interface IVacationRequestService
    {
        Task<IEnumerable<VacationRequestModel>> GetAllAsync();

        Task<IEnumerable<VacationRequestModel>> GetAllByUserAsync(Guid userId);

        Task<VacationRequestModel> GetΒyIdAsync(Guid vacationRequestId);

        Task<VacationRequestModel> AddAsync(VacationRequestModel vacationRequest);

        Task<bool> UpdateStatusAsync(Guid vacationRequestId, RequestStatus status);
    }
}
