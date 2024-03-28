using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Dto;

public class AdCampaignDto
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalBudget { get; set; }
    public PublisherDto Publisher { get; set; }
    public CompanyDto Company { get; set; }
    public AdStatus AdStatus { get; set; }
}
