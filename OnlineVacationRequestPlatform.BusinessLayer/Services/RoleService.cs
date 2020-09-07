using AutoMapper;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using OnlineVacationRequestPlatform.BusinessLayer.Models;
using OnlineVacationRequestPlatform.DataLayer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.BusinessLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            var result = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleModel>>(result);
        }
    }
}
