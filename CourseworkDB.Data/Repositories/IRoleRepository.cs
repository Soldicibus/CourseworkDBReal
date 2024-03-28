using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories;

public interface IRoleRepository
{
    Task<Role> GetRoleAsync(int id);
    Task<Role> GetRoleByNameAsync(string rolename);
    Task<ICollection<Role>> GetRolesAsync();
    Task<ICollection<User>> GetUsersWithRoleAsync(int id);
    bool RoleExists(int id);
}