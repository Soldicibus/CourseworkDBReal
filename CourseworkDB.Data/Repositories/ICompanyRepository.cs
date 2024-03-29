using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories;

public interface ICompanyRepository
{
    bool CompanyExists(int id);
    Task<Company> GetCompanyAsync(int id);
    Task<Company> GetCompanyByEmailAsync(string email);
    Task<Company> GetCompanyByNameAsync(string companyname);
    Task<ICollection<Company>> GetCompaniesAsync();
    Task DeleteCompanyAsync(int id);
    Task<Company> UpdateCompanyAsync(Company role);
    Task<Company> CreateCompanyAsync(Company role);
    bool IsValidEmail(string email);
}