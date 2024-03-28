namespace CourseworkDB.Data.Dto;

public class AdCampaignDto
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalBudget { get; set; }
}
