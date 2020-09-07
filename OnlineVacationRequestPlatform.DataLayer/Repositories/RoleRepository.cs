using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.DataLayer.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConfiguration _configuration;
        private static string _connectionString;

        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            IEnumerable<Role> roles = null;
            try
            {
                var sqlQuery = @"SELECT * FROM [dbo].[Roles]";
                using var connection = new SqlConnection(_connectionString);
                roles = await connection.QueryAsync<Role>(sqlQuery);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roles;
        }
    }
}
