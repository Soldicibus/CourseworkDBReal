using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Dto;

public class AdvertiserDto
{
    public int AdvertiserId { get; set; }
    public UserDto User { get; set; }
    public ICollection<AdCampaignDto> AdCampaigns { get; set; } = new List<AdCampaignDto>();
}