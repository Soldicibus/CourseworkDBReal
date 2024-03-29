﻿using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories;

public interface IRoleRepository
{
    Task<Role> GetRoleAsync(int id);
    Task<Role> GetRoleByNameAsync(string rolename);
    Task<ICollection<Role>> GetRolesAsync();
    Task<ICollection<User>> GetUsersWithRoleAsync(int id);
    Task<Role> CreateRoleAsync(Role role);
    Task DeleteRoleAsync(int id);
    Task<Role> UpdateRoleAsync(Role role);
    bool RoleExists(int id);
}