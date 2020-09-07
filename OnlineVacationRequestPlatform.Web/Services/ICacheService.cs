using OnlineVacationRequestPlatform.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Services
{
    public interface ICacheService
    {
        Task<List<RoleModel>> GetCachedRoleListAsync();
    }
}
