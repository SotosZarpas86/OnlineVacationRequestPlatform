using OnlineVacationRequestPlatform.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.BusinessLayer.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();
    }
}
