using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdCampaignsRepository
    {
        bool AdCampaignExists(int CampaignId);
        Task<AdCampaign> GetAdCampaignAsync(int id);
        Task<AdCampaign> GetAdCampaignByCompanyAsync(int CompanyId);
        Task<AdCampaign> GetAdCampaignByEndDateAsync(DateTime date);
        Task<AdCampaign> GetAdCampaignByNameAsync(string AdCampaign);
        Task<AdCampaign> GetAdCampaignByStartDateAsync(DateTime date);
        Task<AdCampaign> GetAdCampaignByStatusAsync(int StatusId);
        Task DeleteAdCampaignAsync(int id);
        Task<AdCampaign> UpdateAdCampaignAsync(AdCampaign adCampaign);
        Task<AdCampaign> CreateAdCampaignAsync(AdCampaign adCampaign);
        Task<ICollection<AdCampaign>> GetAdCampaignsInDecreasingOrderAsync();
        Task<AdGroup> AddAdGroupToAdCampaignAsync(int adCampaignId, int adGroupId);
        Task<Ad> AddAdToAdCampaignAsync(int adCampaignId, int adId);
        Task<ICollection<AdCampaign>> GetAdCampaignsAsync();
    }
}