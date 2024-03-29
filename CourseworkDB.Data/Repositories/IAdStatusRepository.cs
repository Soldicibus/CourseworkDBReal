using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdStatusRepository
    {
        bool AdStatusExists(int id);
        Task<ICollection<AdStatus>> GetAdStatusesAsync();
        Task<AdStatus> GetAdStatusAsync(int id);
        Task DeleteAdStatusAsync(int id);
        Task<AdStatus> UpdateAdStatusAsync(AdStatus adStatus);
        Task<AdStatus> CreateAdStatusAsync(AdStatus adStatus);
        Task<AdStatus> GetAdStatusByNameAsync(string statusname);
    }
}