using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CourseworkDB.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _ctx;
    public UserRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool UserExists(int id)
    {
        return _ctx.Users.Any(u => u.UserId == id);
    }
    public bool IsValidEmail(string email)
    {
        try
        {
            var o = 0;
            for (var i = 0; i < email.Length; i++)
            {
                if (email[i] == '@') o++;
            }
            if (o != 1)
            {
                return false;
            }
            else return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<ICollection<User>> GetUsersAsync()
    {
        return await _ctx.Users.OrderBy(u => u.UserId).ToListAsync();
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _ctx.Users.FindAsync(id);
    }
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
    }
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<ICollection<Role>> GetRolesOfAUserAsync(int id)
    {
        return await _ctx.UserRoles.Where(a => a.UserId == id).Select(a => a.Role).ToListAsync();
    }
    public async Task<User> CreateUserAsync(User user)
    {
        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync();
        return user;
    }
    public async Task<User> UpdateUserAsync(User user)
    {
        _ctx.Users.Update(user);
        await _ctx.SaveChangesAsync();
        return user;
    }
    public async Task DeleteUserAsync(int id)
    {
        var user = await _ctx.Users.FindAsync(id);
        if (user == null)
        {
            return;
        }

        _ctx.Users.Remove(user);
        await _ctx.SaveChangesAsync();
    }
}
