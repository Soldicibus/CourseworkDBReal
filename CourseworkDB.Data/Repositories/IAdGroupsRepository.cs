using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IAdGroupsRepository
    {
        bool AdGroupExists(int id);
        Task<AdGroup> GetAdGroupAsync(int id);
        Task<AdGroup> GetAdGroupByAudienceAsync(string Audience);
        Task<AdGroup> GetAdGroupByCampaignAsync(int CampaignId);
        Task<AdGroup> GetAdGroupByNameAsync(string AdGroupName);
        Task<ICollection<AdGroup>> GetAdGroupsAsync();
    }
}