using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdTypeRepository
    {
        bool AdTypeExists(int id);
        Task<AdType> GetAdTypeAsync(int id);
        Task<AdType> GetAdTypeByNameAsync(string typename);
        Task<AdType> UpdateAdTypeAsync(AdType adType);
        Task<AdType> CreateAdTypeAsync(AdType adType);
        Task DeleteAdTypeAsync(int id);
        Task<ICollection<AdType>> GetAdTypesAsync();
    }
}