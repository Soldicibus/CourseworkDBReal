using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IUserRoleRepository
    {
        Task<UserRole> AddUserRoleAsync(int UserId, int RoleId);
        Task DelUserRoleAsync(int userId, int roleId);
        Task<ICollection<UserRole>> GetUserRoles();
    }
}