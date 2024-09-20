using AutoMapper;
using KironWebAPI.API.Dtos;
using KironWebAPI.Core.Entities;

namespace KironWebAPI.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
        }
    }
}
