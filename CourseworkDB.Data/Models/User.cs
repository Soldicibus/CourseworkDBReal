using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CourseworkDB.Data.Models;

[Table("Users")]
public class User
{
    [Key]
    [Required]
    public int UserId { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public Role? Role { get; set; }
}
