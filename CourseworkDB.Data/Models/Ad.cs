using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("Ads")]
public class Ad
{
    [Key]
    [Required]
    public int AdId { get; set; }
    public AdCampaign AdCampaign { get; set; }
    [Required]
    public string AdTitle { get; set; }
    public string? AdDescription { get; set; }
    public AdType AdType { get; set; }
    [Required]
    public string AdUrl { get; set; }
}
