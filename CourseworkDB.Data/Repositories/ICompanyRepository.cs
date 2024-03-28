using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories;

public interface ICompanyRepository
{
    bool CompanyExists(int id);
    Task<Company> GetCompanyAsync(int id);
    Task<Company> GetCompanyByEmailAsync(string email);
    Task<Company> GetCompanyByNameAsync(string companyname);
    Task<ICollection<Company>> GetCompaniesAsync();
    bool IsValidEmail(string email);
}