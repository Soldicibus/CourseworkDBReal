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
        CreateMap<Company, CompanyDto>(); CreateMap<CompanyDto, Company>(); CreateMap<Company, CompanyCreationDto>(); CreateMap<CompanyCreationDto, Company>();
        CreateMap<Advertiser, AdvertiserDto>(); CreateMap<AdvertiserDto, Advertiser>(); CreateMap<Advertiser, AdvertiserCreationDto>(); CreateMap<AdvertiserCreationDto, Advertiser>();
        CreateMap<AdCampaign, AdCampaignDto>(); CreateMap<AdCampaignDto, AdCampaign>(); CreateMap<AdCampaign, AdCampaignCreationDto>(); CreateMap<AdCampaignCreationDto, AdCampaign>();
        CreateMap<AdGroup, AdGroupsDto>(); CreateMap<AdGroupsDto, AdGroup>(); //CreateMap<AdGroup, AdGroupsCreationDto>(); CreateMap<AdGroupsCreationDto, AdGroup>();
        CreateMap<Publisher, PublisherDto>(); CreateMap<PublisherDto, Publisher>(); CreateMap<Publisher, PublisherCreationDto>(); CreateMap<PublisherCreationDto, Publisher>();
        CreateMap<Ad, AdDto>(); CreateMap<AdDto, Ad>();
        CreateMap<Payment, PaymentDto>(); CreateMap<PaymentDto, Payment>(); CreateMap<Payment, PaymentCreationDto>(); CreateMap<PaymentCreationDto, Payment>();
        CreateMap<UserRole, UserRoleDto>();
    }
}
