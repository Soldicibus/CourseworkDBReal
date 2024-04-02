using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories;

public interface IUserRepository
{
    bool UserExists(int id);
    Task<ICollection<User>> GetUsersAsync();
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    bool IsValidEmail(string email);
}