using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("AdTypes")]
public class AdType
{
    [Key]
    [Required]
    public int TypeId { get; set; }
    [Required]
    public string TypeName { get; set; }
    [Required]
    public string TypeDesc { get; set; }
}
