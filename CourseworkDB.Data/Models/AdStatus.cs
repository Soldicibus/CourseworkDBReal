using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("AdStatuses")]
public class AdStatus
{
    [Key]
    [Required]
    public int StatusId { get; set; }
    [Required]
    public string StatusName { get; set; }
    [Required]
    public string StatusDesc { get; set; }
}
