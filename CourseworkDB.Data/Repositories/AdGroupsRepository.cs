using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class AdGroupsRepository : IAdGroupsRepository
{
    private readonly DataContext _ctx;
    public AdGroupsRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool AdGroupExists(int id)
    {
        return _ctx.AdGroups.Any(u => u.GroupId == id);
    }
    public async Task<ICollection<AdGroup>> GetAdGroupsAsync()
    {
        return await _ctx.AdGroups.OrderBy(r => r.GroupId).ToListAsync();
    }
    public async Task<AdGroup> GetAdGroupAsync(int id)
    {
        return await _ctx.AdGroups.Include(ac => ac.AdCampaign)
            .FirstOrDefaultAsync(r => r.GroupId == id);
    }
    public async Task<AdGroup> GetAdGroupByNameAsync(string AdGroupName)
    {
        return await _ctx.AdGroups
            .Include(ac => ac.AdCampaign)
            .FirstOrDefaultAsync(r => r.GroupName == AdGroupName);
    }
    public async Task<AdGroup> GetAdGroupByCampaignAsync(int CampaignId)
    {
        return await _ctx.AdGroups
            .AsNoTracking()
            .Include(ac => ac.AdCampaign)
            .FirstOrDefaultAsync(r => r.AdCampaign.CampaignId == CampaignId);
    }
    public async Task<AdGroup> GetAdGroupByAudienceAsync(string Audience)
    {
        return await _ctx.AdGroups.AsNoTracking()
            .Include(ac => ac.AdCampaign)
            .FirstOrDefaultAsync(r => r.Audience == Audience);
    }
}
