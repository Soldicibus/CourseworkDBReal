using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdRepository
    {
        bool AdExists(int Id);
        Task<ICollection<Ad>> GetAdsAsync();
        Task<Ad> GetAdsByCampaignAsync(int Id);
        Task<Ad> GetAdsByIdAsync(int Id);
        Task<Ad> GetAdsByTitleAsync(string title);
        Task<Ad> GetAdsByTypeAsync(int Id);
        Task DeleteAdAsync(int adId);
        Task<Ad> UpdateAdAsync(Ad ad);
        Task<Ad> CreateAdAsync(Ad ad);
    }
}