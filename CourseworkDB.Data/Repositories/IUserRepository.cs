//using CourseworkDB.Data.Interfaces;
using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        bool IsValidEmail(string email);
    }
}