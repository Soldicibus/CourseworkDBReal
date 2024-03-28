using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdStatusRepository
    {
        bool AdStatusExists(int id);
        Task<ICollection<AdStatus>> GetAdStatusesAsync();
        Task<AdStatus> GetAdStatusAsync(int id);
        Task<AdStatus> GetAdStatusByNameAsync(string statusname);
    }
}