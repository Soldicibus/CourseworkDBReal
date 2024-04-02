using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("Roles")]
public class Role
{
    [Key]
    [Required]
    public int RoleId { get; set; }
    [Required]
    public string RoleName { get; set; }
    public ICollection<User>? Users { get; set; } = new List<User>();
}
