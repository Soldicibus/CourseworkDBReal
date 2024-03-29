using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdGroupsRepository
    {
        bool AdGroupExists(int id);
        Task<AdGroup> GetAdGroupAsync(int id);
        Task<AdGroup> GetAdGroupByAudienceAsync(string Audience);
        Task<AdGroup> GetAdGroupByNameAsync(string AdGroupName);
        Task DeleteAdGroupsAsync(int adGroupId);
        Task<AdGroup> UpdateAdGroupAsync(AdGroup adGroup);
        Task<AdGroup> AddAdGroupAsync(AdGroup adGroup);
        Task<AdCampaign> AddAdCampaignToAdGroupAsync(int adGroupId, int adCampaignId);
        Task<ICollection<AdGroup>> GetAdGroupsInDecreasingOrderAsync();
        Task<ICollection<AdGroup>> GetAdGroupsAsync();
    }
}