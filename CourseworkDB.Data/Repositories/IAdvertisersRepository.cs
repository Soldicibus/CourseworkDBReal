using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdvertisersRepository
    {
        bool AdvertiserExist(int AdvertiserId);
        Task<Advertiser> GetAdvertiserByIdAsync(int id);
        Task<IEnumerable<Advertiser>> GetAllAdvertisersAsync();
        Task<IEnumerable<Advertiser>> GetAdvertisersByUserId(int userId);
    }
}