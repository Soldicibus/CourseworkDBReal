using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class AdvertisersRepository : IAdvertisersRepository
{
    private readonly DataContext _ctx;
    public AdvertisersRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool AdvertiserExist(int AdvertiserId)
    {
        return _ctx.Advertisers.Any(r => r.AdvertiserId == AdvertiserId);
    }
    public async Task<IEnumerable<Advertiser>> GetAllAdvertisersAsync()
    {
        return await _ctx.Advertisers.Include(a => a.User).ToListAsync();
    }
    public async Task<Advertiser> GetAdvertiserByIdAsync(int id)
    {
        return await _ctx.Advertisers.Include(a => a.User).FirstOrDefaultAsync(a => a.AdvertiserId == id);
    }
    public async Task<IEnumerable<Advertiser>> GetAdvertisersByUserId(int userId)
    {
        return await _ctx.Advertisers
        .Include(a => a.User)
        .Where(a => a.User.UserId == userId).ToListAsync();
    }
}
