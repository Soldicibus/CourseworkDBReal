using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly DataContext _ctx;
    public UserRoleRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<ICollection<UserRole>> GetUserRoles()
    {
        return await _ctx.UserRoles.OrderBy(u => u.UserId).ToListAsync();
    }
    public async Task<UserRole> AddUserRoleAsync(int UserId, int RoleId)
    {
        var user = await _ctx.Users.FindAsync(UserId);
        var role = await _ctx.Roles.FindAsync(RoleId);
        if (role == null || user == null)
        {
            return null;
        }
        var existingUserRole = await _ctx.UserRoles
        .FirstOrDefaultAsync(ur => ur.UserId == UserId && ur.RoleId == RoleId);
        if (existingUserRole != null)
        {
            return null;
        }
        var userRole = new UserRole
        {
            UserId = UserId,
            RoleId = RoleId
        };
        _ctx.UserRoles.Add(userRole);
        await _ctx.SaveChangesAsync();

        return userRole;
    }
    public async Task DelUserRoleAsync(int userId, int roleId)
    {
        var userRole = await _ctx.UserRoles
        .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (userRole == null)
        {
            return;
        }
        _ctx.UserRoles.Remove(userRole);
        await _ctx.SaveChangesAsync();
        return;
    }
}
