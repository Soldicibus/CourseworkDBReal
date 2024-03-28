using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

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
}
