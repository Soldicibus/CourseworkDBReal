using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseworkDB.Data.Models;

[Table("Publishers")]
public class Publisher
{
    [Key]
    [Required]
    public int PublisherId { get; set; }
    public User User { get; set; }
    public ICollection<Company> Companies { get; set; }
    public string? WebsiteURL { get; set; }
}