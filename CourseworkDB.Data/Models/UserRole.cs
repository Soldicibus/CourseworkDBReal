using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("UserRole")]
public class UserRole
{
    [Key]
    [Required]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
    [Key]
    [Required]
    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role? Role { get; set; }
}
