using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

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
        return await _ctx.Companies
        .Include(p => p.Publisher)
            .ThenInclude(u => u.User)
        .OrderBy(r => r.CompanyId)
        .ToListAsync();

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
    public async Task<Company> CreateCompanyAsync(Company company)
    {
        var publisher = await _ctx.Publishers.FindAsync(company.Publisher.PublisherId);
        if (publisher == null)
        {
            return null;
        }
        company.Publisher = publisher;

        _ctx.Companies.Add(company);
        await _ctx.SaveChangesAsync();
        return company;
    }
    public async Task<Company> UpdateCompanyAsync(Company company)
    {
        var publisher = await _ctx.Publishers.FindAsync(company.Publisher.PublisherId);
        if (publisher == null)
        {
            return null;
        }
        company.Publisher = publisher;
        _ctx.Companies.Update(company);
        await _ctx.SaveChangesAsync();
        return company;
    }
    public async Task DeleteCompanyAsync(int id)
    {
        var company = await _ctx.Companies.FindAsync(id);
        if (company == null)
        {
            return;
        }

        var advertiserWithCompany = await _ctx.Advertisers.Where(a => a.Company.CompanyId == id).ToListAsync();
        foreach (var advertiser in advertiserWithCompany)
        {
            advertiser.Company = null;
        }

        _ctx.Companies.Remove(company);
        await _ctx.SaveChangesAsync();
    }
    public async Task<Advertiser> AddAdvertiserToCompanyAsync(int companyId, int advertiserId)
    {
        var company = await _ctx.Companies.FindAsync(companyId);
        var advertiser = await _ctx.Advertisers.FindAsync(advertiserId);
        if (company == null || advertiser == null)
        {
            throw new ArgumentException("Record not found");
        }
        company.Advertisers.Add(advertiser);
        await _ctx.SaveChangesAsync();
        return advertiser;
    }
}
