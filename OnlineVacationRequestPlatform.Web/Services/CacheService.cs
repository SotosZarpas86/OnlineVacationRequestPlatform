using Microsoft.Extensions.Caching.Memory;
using OnlineVacationRequestPlatform.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Services
{
    public class CacheService : ICacheService
    {
        private readonly UserService _userService;
        private readonly IMemoryCache _memoryCache;

        public CacheService(UserService userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        public async Task<List<RoleModel>> GetCachedRoleListAsync()
        {
            if (!_memoryCache.TryGetValue("Roles", out List<RoleModel> roles))
            {
                roles = await _userService.GetRoleListAsync();
                _memoryCache.Set("Roles", roles, TimeSpan.FromHours(12));
            }
            return roles;
        }
    }
}
