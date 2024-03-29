using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>(); CreateMap<User, UserDto_wo_Id>(); CreateMap<User, UserDto_w_pass>(); CreateMap<UserDto_w_pass, User>();
        CreateMap<Role, RoleDto>(); CreateMap<RoleDto, Role>();
        CreateMap<Company, CompanyDto>(); CreateMap<CompanyDto, Company>();
        CreateMap<Advertiser, AdvertiserDto>();
        CreateMap<AdCampaign, AdCampaignDto>();
        CreateMap<AdGroup, AdGroupsDto>();
        CreateMap<Publisher, PublisherDto>();
        CreateMap<Ad, AdDto>();
        CreateMap<Payment, PaymentDto>();
    }
}
