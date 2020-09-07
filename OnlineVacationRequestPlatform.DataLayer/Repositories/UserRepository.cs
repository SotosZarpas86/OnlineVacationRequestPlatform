using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.ExtendedEntities;
using OnlineVacationRequestPlatform.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        private static string _connectionString;

        public UserRepository(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IEnumerable<User> users = null;
            try
            {
                var sqlQuery = @"SELECT * FROM [dbo].[Users]";
                using var connection = new SqlConnection(_connectionString);
                users = await connection.QueryAsync<User>(sqlQuery);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public async Task<IEnumerable<ExtendedUser>> GetExtendedAllAsync()
        {
            IEnumerable<ExtendedUser> users = null;
            try
            {
                var sqlQuery = @"SELECT U.Id, U.FirstName, U.LastName, U.Email, R.Name AS RoleName 
                                 FROM [dbo].[Users] AS U 
                                 INNER JOIN [dbo].[Roles] AS R ON U.RoleId = R.Id";
                using var connection = new SqlConnection(_connectionString);
                users = await connection.QueryAsync<ExtendedUser>(sqlQuery);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public async Task<User> GetΒyIdAsync(Guid userId)
        {
            User user = null;
            try
            {
                var sqlQuery = @"SELECT * FROM [dbo].[Users] WHERE Id=@userId";
                using var connection = new SqlConnection(_connectionString);
                user = await connection.QuerySingleOrDefaultAsync<User>(sqlQuery, new { userId });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<ExtendedSingleUser> GetΒyEmailAsync(string email)
        {
            ExtendedSingleUser user = null;
            try
            {
                var sqlQuery = @"SELECT U.Id, U.FirstName, U.LastName, U.Email, U.Password, R.Name AS RoleName, U.SupervisorId 
                                 FROM [dbo].[Users] AS U 
                                 INNER JOIN [dbo].[Roles] AS R ON U.RoleId = R.Id
                                 WHERE Email=@email";
                using var connection = new SqlConnection(_connectionString);
                user = await connection.QuerySingleOrDefaultAsync<ExtendedSingleUser>(sqlQuery, new { email });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<User> AddAsync(User user)
        {
            try
            {
                await _applicationDbContext.Users.AddAsync(user);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            try
            {
                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var deleteStatus = false;
            try
            {
                var dbRow = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (dbRow != null)
                {
                    _applicationDbContext.Users.Remove(dbRow);
                    await _applicationDbContext.SaveChangesAsync();
                    deleteStatus = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return deleteStatus;
        }
    }
}
