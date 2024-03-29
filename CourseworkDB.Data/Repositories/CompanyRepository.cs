using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DataContext _ctx;
    public CompanyRepository(DataContext ctx)
    {
        _ctx = ctx;
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
    public bool CompanyExists(int id)
    {
        return _ctx.Companies.Any(r => r.CompanyId == id);
    }
    public async Task<ICollection<Company>> GetCompaniesAsync()
    {
        return await _ctx.Companies.OrderBy(r => r.CompanyId).ToListAsync();
    }
    public async Task<Company> GetCompanyAsync(int id)
    {
        return await _ctx.Companies.FindAsync(id);
    }
    public async Task<Company> GetCompanyByNameAsync(string companyname)
    {
        return await _ctx.Companies.AsNoTracking().FirstOrDefaultAsync(r => r.CompanyName == companyname);
    }
    public async Task<Company> GetCompanyByEmailAsync(string email)
    {
        return await _ctx.Companies.AsNoTracking().FirstOrDefaultAsync(u => u.CompanyEmail == email);
    }
    public async Task<Company> CreateCompanyAsync(Company role)
    {
        _ctx.Companies.Add(role);
        await _ctx.SaveChangesAsync();
        return role;
    }
    public async Task<Company> UpdateCompanyAsync(Company role)
    {
        _ctx.Companies.Update(role);
        await _ctx.SaveChangesAsync();
        return role;
    }
    public async Task DeleteCompanyAsync(int id)
    {
        var role = await _ctx.Companies.FindAsync(id);
        if (role == null)
        {
            return;
        }

        _ctx.Companies.Remove(role);
        await _ctx.SaveChangesAsync();
    }
}
