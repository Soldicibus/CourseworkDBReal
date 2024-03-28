using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class AdStatusRepository : IAdStatusRepository
{
    private readonly DataContext _ctx;
    public AdStatusRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool AdStatusExists(int id)
    {
        return _ctx.AdStatuses.Any(u => u.StatusId == id);
    }
    public async Task<ICollection<AdStatus>> GetAdStatusesAsync()
    {
        return await _ctx.AdStatuses.OrderBy(r => r.StatusId).ToListAsync();
    }
    public async Task<AdStatus> GetAdStatusAsync(int id)
    {
        return await _ctx.AdStatuses.FindAsync(id);
    }
    public async Task<AdStatus> GetAdStatusByNameAsync(string statusname)
    {
        return await _ctx.AdStatuses.AsNoTracking().FirstOrDefaultAsync(r => r.StatusName == statusname);
    }
    //AdCampaign by Status
}