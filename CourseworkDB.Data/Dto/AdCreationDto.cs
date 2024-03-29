using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Dto;

public class AdCreationDto
{
    public int AdId { get; set; }
    public int AdCampaignId { get; set; }
    public string AdTitle { get; set; }
    public string? AdDescription { get; set; }
    public int AdTypeId { get; set; }
    public string AdUrl { get; set; }

}
