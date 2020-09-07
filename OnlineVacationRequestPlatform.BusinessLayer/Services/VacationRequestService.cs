using AutoMapper;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using OnlineVacationRequestPlatform.BusinessLayer.Models;
using OnlineVacationRequestPlatform.BusinessLayer.Utilities;
using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.BusinessLayer.Services
{
    public class VacationRequestService : IVacationRequestService
    {
        private readonly IVacationRequestRepository _vacationRequestRepository;
        private readonly IMapper _mapper;

        public VacationRequestService(IVacationRequestRepository vacationRequestRepository, IMapper mapper)
        {
            _vacationRequestRepository = vacationRequestRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VacationRequestModel>> GetAllAsync()
        {
            var result = await _vacationRequestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VacationRequestModel>>(result);
        }

        public async Task<IEnumerable<VacationRequestModel>> GetAllByUserAsync(Guid userId)
        {
            var result = await _vacationRequestRepository.GetAllByUserAsync(userId);
            return _mapper.Map<IEnumerable<VacationRequestModel>>(result);
        }

        public async Task<VacationRequestModel> GetΒyIdAsync(Guid vacationRequestId)
        {
            var result = await _vacationRequestRepository.GetΒyIdAsync(vacationRequestId);
            return _mapper.Map<VacationRequestModel>(result);
        }
        public async Task<VacationRequestModel> AddAsync(VacationRequestModel vacationRequest)
        {
            var vacationRequestDb = _mapper.Map<VacationRequest>(vacationRequest);
            PopulateSystemicFields(vacationRequestDb, vacationRequest.UserId, vacationRequest.UserId, DateTime.Now, DateTime.Now);
            var result = await _vacationRequestRepository.AddAsync(vacationRequestDb);
            return _mapper.Map<VacationRequestModel>(result);
        }

        public async Task<bool> UpdateStatusAsync(Guid vacationRequestId, RequestStatus status)
        {
            var convertedStatus = (DataLayer.Utilities.RequestStatus)status;
            return await _vacationRequestRepository.UpdateStatusAsync(vacationRequestId, convertedStatus);
        }

        private void PopulateSystemicFields(VacationRequest vacationRequest, Guid creatorUserId, Guid modifierUserId, DateTime dateAdded, DateTime dateModified)
        {
            vacationRequest.CreatorUserId = creatorUserId;
            vacationRequest.ModifierUserId = modifierUserId;
            vacationRequest.DateAdded = dateAdded;
            vacationRequest.DateModified = dateModified;
        }

        private void UpdateSystemicFields(VacationRequest vacationRequest, Guid modifierUserId, DateTime dateModified)
        {
            vacationRequest.ModifierUserId = modifierUserId;
            vacationRequest.DateModified = dateModified;
        }
    }
}
