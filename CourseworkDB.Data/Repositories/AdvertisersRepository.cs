using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

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
        var advertisers = await _ctx.Advertisers
            .Include(a => a.User)
            .Include(a => a.Company)
            .ToListAsync();

        var result = advertisers.Select(a => new Advertiser
        {
            AdvertiserId = a.AdvertiserId,
            User = new User { UserName = a.User.UserName },
            Company = a.Company != null ? new Company { CompanyName = a.Company.CompanyName } : null
        });

        return result;
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
    public async Task<Advertiser> AddAdvertiserAsync(Advertiser advertiser)
    {
        var user = await _ctx.Users.FindAsync(advertiser.User.UserId);
        if (user == null)
        {
            return null;
        }
        var userAlreadyHere = await _ctx.Advertisers.AnyAsync(p => p.User.UserId == advertiser.User.UserId);
        if (userAlreadyHere)
        {
            return null;
        }
        advertiser.User = user;

        _ctx.Advertisers.Add(advertiser);
        await _ctx.SaveChangesAsync();

        return advertiser;
    }
    public async Task<Advertiser> UpdateAdvertiserAsync(Advertiser advertiser)
    {
        var user = await _ctx.Users.FindAsync(advertiser.User.UserId);
        if (user == null)
        {
            return null;
        }
        advertiser.User = user;

        _ctx.Advertisers.Update(advertiser);
        await _ctx.SaveChangesAsync();

        return advertiser;
    }
    public async Task DeleteAdvertisersAsync(int advertiserId)
    {
        var advertiser = await _ctx.Advertisers.FindAsync(advertiserId);
        if (advertiser == null)
        {
            return;
        }

        _ctx.Advertisers.Remove(advertiser);
        await _ctx.SaveChangesAsync();
        return;
    }
}
