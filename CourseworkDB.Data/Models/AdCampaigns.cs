using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("AdCampaigns")]
public class AdCampaign
{
    [Key]
    [Required]
    public int CampaignId { get; set; }
    [Required]
    public string CampaignName { get; set;}
    [Required]
    public DateTime StartDate { get; set;}
    [Required]
    public DateTime EndDate { get; set;}
    [Required]
    public float TotalBudget { get; set;}
    public Publisher Publisher { get; set; }
    public Company Company { get; set; }
    public AdStatus AdStatus { get; set; }
    public ICollection<AdGroup> AdGroups { get; set; } = new List<AdGroup>();
    public ICollection<Ad> Ads { get; set; } = new List<Ad>();
}