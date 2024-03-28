using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("Companies")]
public class Company
{
    [Key]
    [Required]
    public int CompanyId { get; set; }
    [Required]
    public string CompanyName { get; set;}
    [Required]
    public string CompanyEmail { get; set; }
    public string? CompanyPhone { get; set; }
    public ICollection<Advertiser> Advertisers { get; set; } = new List<Advertiser>();
    public ICollection<Publisher> Publishers { get; set; } = new List<Publisher>();
}
