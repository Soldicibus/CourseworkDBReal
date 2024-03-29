using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly DataContext _ctx;
    public RoleRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool RoleExists(int id)
    {
        return _ctx.Roles.Any(r => r.RoleId == id);
    }
    public async Task<ICollection<Role>> GetRolesAsync()
    {
        return await _ctx.Roles.OrderBy(r => r.RoleId).ToListAsync();
    }
    public async Task<Role> GetRoleAsync(int id)
    {
        return await _ctx.Roles.FindAsync(id);
    }
    public async Task<Role> GetRoleByNameAsync(string rolename)
    {
        return await _ctx.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.RoleName == rolename);
    }
    public async Task<ICollection<User>> GetUsersWithRoleAsync(int id)
    {
        return await _ctx.UserRoles.Where(a => a.RoleId == id).Select(a => a.User).ToListAsync();
    }
    public async Task<Role> CreateRoleAsync(Role role)
    {
        _ctx.Roles.Add(role);
        await _ctx.SaveChangesAsync();
        return role;
    }
    public async Task<Role> UpdateRoleAsync(Role role)
    {
        _ctx.Roles.Update(role);
        await _ctx.SaveChangesAsync();
        return role;
    }
    public async Task DeleteRoleAsync(int id)
    {
        var role = await _ctx.Roles.FindAsync(id);
        if (role == null)
        {
            return;
        }

        _ctx.Roles.Remove(role);
        await _ctx.SaveChangesAsync();
    }
}
