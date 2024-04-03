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
        return await _ctx.AdGroups.FirstOrDefaultAsync(r => r.GroupId == id);
    }
    public async Task<AdGroup> GetAdGroupByNameAsync(string AdGroupName)
    {
        return await _ctx.AdGroups.FirstOrDefaultAsync(r => r.GroupName == AdGroupName);
    }
    public async Task<AdGroup> GetAdGroupByAudienceAsync(string Audience)
    {
        return await _ctx.AdGroups.AsNoTracking().FirstOrDefaultAsync(r => r.Audience == Audience);
    }
    public async Task<ICollection<AdGroup>> GetAdGroupsInDecreasingOrderAsync()
    {
        return await _ctx.AdGroups.OrderByDescending(p => p.BidAmount).ToListAsync();
    }
    public async Task<AdGroup> AddAdGroupAsync(AdGroup adGroup)
    {
        _ctx.AdGroups.Add(adGroup);
        await _ctx.SaveChangesAsync();

        return adGroup;
    }
    public async Task<AdGroup> UpdateAdGroupAsync(AdGroup adGroup)
    {
        _ctx.AdGroups.Update(adGroup);
        await _ctx.SaveChangesAsync();

        return adGroup;
    }
    public async Task DeleteAdGroupsAsync(int adGroupId)
    {
        var adGroup = await _ctx.AdGroups.FindAsync(adGroupId);
        if (adGroup == null)
        {
            return;
        }

        var campaignsWithGroups = await _ctx.AdCampaigns.Where(a => a.AdGroup.GroupId == adGroupId).ToListAsync();
        foreach (var campaign in campaignsWithGroups)
        {
            campaign.AdGroup = null;
        }

        _ctx.AdGroups.Remove(adGroup);
        await _ctx.SaveChangesAsync();
        return;
    }
    public async Task<AdCampaign> AddAdCampaignToAdGroupAsync(int adGroupId, int adCampaignId)
    {
        var adCampaign = await _ctx.AdCampaigns.FindAsync(adCampaignId);
        var adGroup = await _ctx.AdGroups.FindAsync(adGroupId);
        if (adCampaign == null || adGroup == null)
        {
            return null;
        }
        adGroup.AdCampaigns.Add(adCampaign);
        await _ctx.SaveChangesAsync();
        return adCampaign;
    }
}