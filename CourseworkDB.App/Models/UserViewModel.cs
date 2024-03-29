using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CourseworkDB.App.Models;

public class UserViewModel
{
    public int UserId { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    //public ICollection<UserRole>? UserRoles { get; set; } = new List<UserRole>();
}
