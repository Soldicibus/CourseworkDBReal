using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Dto;

public class AdCampaignCreationDto
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalBudget { get; set; }
    public int PublisherId { get; set; }
    public int CompanyId { get; set; }
    public int AdStatusId { get; set; }
    public int AdGroupId { get; set; }
}
