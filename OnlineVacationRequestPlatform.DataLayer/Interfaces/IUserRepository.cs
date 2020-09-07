using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.ExtendedEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.DataLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<IEnumerable<ExtendedUser>> GetExtendedAllAsync();

        Task<User> GetΒyIdAsync(Guid userId);

        Task<ExtendedSingleUser> GetΒyEmailAsync(string email);

        Task<User> AddAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<bool> DeleteAsync(Guid userId);
    }
}
