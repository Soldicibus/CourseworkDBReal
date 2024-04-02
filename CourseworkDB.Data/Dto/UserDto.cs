namespace CourseworkDB.Data.Dto;

public class UserDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public RoleDto Role { get; set; }
}
