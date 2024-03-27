using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserDto_wo_Id>();
    }
}
