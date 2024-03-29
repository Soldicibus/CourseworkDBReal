using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class AdCampaignsRepository : IAdCampaignsRepository
{
    private readonly DataContext _ctx;
    public AdCampaignsRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool AdCampaignExists(int CampaignId)
    {
        return _ctx.AdCampaigns.Any(u => u.CampaignId == CampaignId);
    }
    public async Task<ICollection<AdCampaign>> GetAdCampaignsAsync()
    {
        return await _ctx.AdCampaigns.OrderBy(r => r.CampaignId).ToListAsync();
    }
    public async Task<AdCampaign> GetAdCampaignAsync(int id)
    {
        return await _ctx.AdCampaigns.Include(ac => ac.Publisher).Include(ac => ac.Company).Include(ac => ac.AdStatus)
            .FirstOrDefaultAsync(r => r.CampaignId == id);
    }
    public async Task<AdCampaign> GetAdCampaignByNameAsync(string AdCampaignName)
    {
        return await _ctx.AdCampaigns
            .Include(ac => ac.Publisher)
            .Include(ac => ac.Company)
            .Include(ac => ac.AdStatus)
            .FirstOrDefaultAsync(r => r.CampaignName == AdCampaignName);
    }
    public async Task<AdCampaign> GetAdCampaignByStartDateAsync(DateTime date)
    {
        return await _ctx.AdCampaigns
            .AsNoTracking()
            .Include(ac => ac.Publisher)
            .Include(ac => ac.Company)
            .Include(ac => ac.AdStatus)
            .FirstOrDefaultAsync(r => r.StartDate == date);
    }
    public async Task<AdCampaign> GetAdCampaignByEndDateAsync(DateTime date)
    {
        return await _ctx.AdCampaigns.AsNoTracking()
            .Include(ac => ac.Publisher)
            .Include(ac => ac.Company)
            .Include(ac => ac.AdStatus)
            .FirstOrDefaultAsync(r => r.EndDate == date);
    }
    public async Task<AdCampaign> GetAdCampaignByCompanyAsync(int CompanyId)
    {
        return await _ctx.AdCampaigns
            .AsNoTracking()
            .Include(ac => ac.Publisher)
            .Include(ac => ac.Company)
            .Include(ac => ac.AdStatus)
            .FirstOrDefaultAsync(r => r.Company.CompanyId == CompanyId);
    }
    public async Task<AdCampaign> GetAdCampaignByStatusAsync(int StatusId)
    {
        return await _ctx.AdCampaigns
            .AsNoTracking()
            .Include(ac => ac.Publisher)
            .Include(ac => ac.Company)
            .Include(ac => ac.AdStatus)
            .AsNoTracking().FirstOrDefaultAsync(r => r.AdStatus.StatusId == StatusId);
    }
    public async Task<ICollection<AdCampaign>> GetAdCampaignsInDecreasingOrderAsync()
    {
        return await _ctx.AdCampaigns.OrderByDescending(p => p.TotalBudget).ToListAsync();
    }
}
