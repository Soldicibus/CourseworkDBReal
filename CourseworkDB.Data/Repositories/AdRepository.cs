using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace CourseworkDB.Data.Repositories;

public class AdRepository : IAdRepository
{
    private readonly DataContext _ctx;
    public AdRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool AdExists(int Id)
    {
        return _ctx.Ads.Any(a => a.AdId == Id);
    }
    public async Task<ICollection<Ad>> GetAdsAsync()
    {
        return await _ctx.Ads.OrderBy(r => r.AdId).ToListAsync();
    }
    public async Task<Ad> GetAdsByIdAsync(int Id)
    {
        return await _ctx.Ads.Include(a => a.AdType).Include(a => a.AdCampaign).FirstOrDefaultAsync(r => r.AdId == Id);
    }
    public async Task<Ad> GetAdsByCampaignAsync(int Id)
    {
        return await _ctx.Ads.Include(a => a.AdType).Include(a => a.AdCampaign).FirstOrDefaultAsync(r => r.AdCampaign.CampaignId == Id);
    }
    public async Task<Ad> GetAdsByTypeAsync(int Id)
    {
        return await _ctx.Ads.Include(a => a.AdType).Include(a => a.AdCampaign).FirstOrDefaultAsync(r => r.AdType.TypeId == Id);
    }
    public async Task<Ad> GetAdsByTitleAsync(string title)
    {
        return await _ctx.Ads.Include(a => a.AdType).Include(a => a.AdCampaign).FirstOrDefaultAsync(r => r.AdTitle == title);
    }
    public async Task<Ad> CreateAdAsync(Ad ad)
    {
        var campaign = await _ctx.AdCampaigns.FindAsync(ad.AdCampaign.CampaignId);
        var type = await _ctx.AdTypes.FindAsync(ad.AdType.TypeId);
        if (campaign == null || type == null)
        {
            return null;
        }
        ad.AdCampaign = campaign;
        ad.AdType = type;

        _ctx.Ads.Add(ad);
        await _ctx.SaveChangesAsync();
        return ad;
    }
    public async Task<Ad> UpdateAdAsync(Ad ad)
    {
        var existingAd = await _ctx.Ads.FindAsync(ad.AdId);
        if (existingAd == null)
        {
            return null;
        }
        var campaign = await _ctx.AdCampaigns.FindAsync(ad.AdCampaign.CampaignId);
        var type = await _ctx.AdTypes.FindAsync(ad.AdType.TypeId);
        if (campaign == null || type == null)
        {
            return null;
        }
        existingAd.AdType = type;
        existingAd.AdCampaign = campaign;

        _ctx.Ads.Update(existingAd);
        await _ctx.SaveChangesAsync();
        return existingAd;
    }
    public async Task DeleteAdAsync(int adId)
    {
        var ad = await _ctx.Ads.FindAsync(adId);
        if (ad == null)
        {
            return;
        }

        _ctx.Ads.Remove(ad);
        await _ctx.SaveChangesAsync();
        return;
    }
}
