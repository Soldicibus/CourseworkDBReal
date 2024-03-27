using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("Advertisers")]
public class Advertiser
{
    [Key]
    [Required]
    public int AdvertiserId { get; set; }
    public User User { get; set; }
    public ICollection<Company> Companies { get; set; } = new List<Company>();
}
