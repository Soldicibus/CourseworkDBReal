using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Dto;

public class Ad_wo_CampaignDto
{
    public int AdId { get; set; }
    public string AdTitle { get; set; }
    public string? AdDescription { get; set; }
    public AdType AdType { get; set; }
    public string AdUrl { get; set; }
}
