using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdTypeRepository
    {
        bool AdTypeExists(int id);
        Task<AdType> GetAdTypeAsync(int id);
        Task<AdType> GetAdTypeByNameAsync(string typename);
        Task<ICollection<AdType>> GetAdTypesAsync();
    }
}