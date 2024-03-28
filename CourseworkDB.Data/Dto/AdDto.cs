using CourseworkDB.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseworkDB.Data.Dto;

public class AdDto
{
    public int AdId { get; set; }
    public AdCampaignDto AdCampaign { get; set; }
    public string AdTitle { get; set; }
    public string? AdDescription { get; set; }
    public AdType AdType { get; set; }
    public string AdUrl { get; set; }
}
