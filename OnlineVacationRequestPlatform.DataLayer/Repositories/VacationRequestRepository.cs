using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.Interfaces;
using OnlineVacationRequestPlatform.DataLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.DataLayer.Repositories
{
    public class VacationRequestRepository : IVacationRequestRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        private static string _connectionString;

        public VacationRequestRepository(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<IEnumerable<VacationRequest>> GetAllAsync()
        {
            IEnumerable<VacationRequest> vacationRequests = null;
            try
            {
                var sqlQuery = @"SELECT * FROM [dbo].[VacationRequests]";
                using var connection = new SqlConnection(_connectionString);
                vacationRequests = await connection.QueryAsync<VacationRequest>(sqlQuery);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vacationRequests;
        }

        public async Task<IEnumerable<VacationRequest>> GetAllByUserAsync(Guid userId)
        {
            IEnumerable<VacationRequest> vacationRequests = null;
            try
            {
                var sqlQuery = @"SELECT * FROM [dbo].[VacationRequests] WHERE UserId=@userId";
                using var connection = new SqlConnection(_connectionString);
                vacationRequests = await connection.QueryAsync<VacationRequest>(sqlQuery, new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vacationRequests;
        }

        public async Task<VacationRequest> GetΒyIdAsync(Guid vacationRequestId)
        {
            VacationRequest vacationRequest = null;
            try
            {
                var sqlQuery = @"SELECT * FROM [dbo].[VacationRequests] WHERE Id=@vacationRequestId";
                using var connection = new SqlConnection(_connectionString);
                vacationRequest = await connection.QuerySingleOrDefaultAsync<VacationRequest>(sqlQuery, new { vacationRequestId });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vacationRequest;
        }

        public async Task<VacationRequest> AddAsync(VacationRequest vacationRequest)
        {
            try
            {
                await _applicationDbContext.VacationRequests.AddAsync(vacationRequest);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vacationRequest;
        }

        public async Task<bool> UpdateStatusAsync(Guid vacationRequestId, RequestStatus status)
        {
            var updateStatus = false;
            try
            {
                var vacationRequest = await _applicationDbContext.VacationRequests.SingleOrDefaultAsync(vr => vr.Id == vacationRequestId);
                if (vacationRequest != null)
                {
                    vacationRequest.RequestStatus = status;
                    _applicationDbContext.VacationRequests.Update(vacationRequest);
                    await _applicationDbContext.SaveChangesAsync();
                    updateStatus = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return updateStatus;
        }
    }
}
