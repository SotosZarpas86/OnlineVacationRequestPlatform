using AutoMapper;
using OnlineVacationRequestPlatform.BusinessLayer.Models;
using OnlineVacationRequestPlatform.DataLayer.Entities;
using OnlineVacationRequestPlatform.DataLayer.ExtendedEntities;

namespace OnlineVacationRequestPlatform.BusinessLayer.Utilities
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<ExtendedUser, ExtendedUserModel>().ReverseMap();
            CreateMap<ExtendedSingleUser, ExtendedSingleUserModel>().ReverseMap();
            CreateMap<VacationRequest, VacationRequestModel>().ReverseMap();
        }
    }
}
