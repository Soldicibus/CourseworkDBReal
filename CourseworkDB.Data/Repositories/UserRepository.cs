﻿using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _ctx;
    public UserRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool IsValidEmail(string email)
    {
        try
        {
            var emailcheck = '@';
            var o = 0;
            for (var i = 0; i < email.Length; i++)
            {
                if (email[i] == emailcheck) o++;
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
    /*public async Task<User> CreateUserAsync(User user)
    {

    }*/
}
