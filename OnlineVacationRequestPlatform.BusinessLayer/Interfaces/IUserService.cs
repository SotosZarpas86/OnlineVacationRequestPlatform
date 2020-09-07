using OnlineVacationRequestPlatform.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllAsync();

        Task<IEnumerable<ExtendedUserModel>> GetExtendedAllAsync();

        Task<UserModel> GetΒyIdAsync(Guid userId);

        Task<UserModel> AddAsync(UserModel user);

        Task<UserModel> UpdateAsync(UserModel user);

        Task<bool> DeleteAsync(Guid userId);

        Task<ExtendedSingleUserModel> AuthenticateUserAsync(string email, string password, string roleName);
    }
}
