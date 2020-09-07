using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.DataLayer.Interfaces
{
    public interface IVacationRequestRepository
    {
        Task<IEnumerable<VacationRequest>> GetAllAsync();

        Task<IEnumerable<VacationRequest>> GetAllByUserAsync(Guid userId);

        Task<VacationRequest> GetΒyIdAsync(Guid vacationRequestId);

        Task<VacationRequest> AddAsync(VacationRequest vacationRequest);

        Task<bool> UpdateStatusAsync(Guid vacationRequestId, RequestStatus status);
    }
}
