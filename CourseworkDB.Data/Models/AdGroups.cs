using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("AdGroups")]
public class AdGroup
{
    [Key]
    [Required]
    public int GroupId { get; set; }
    [Required]
    public string GroupName { get; set; }
    [Required]
    public string Audience { get; set; }
    public string? Description { get; set; }
    public AdCampaign AdCampaign { get; set; }
    [Required]
    public float BidAmount { get; set; }
}
