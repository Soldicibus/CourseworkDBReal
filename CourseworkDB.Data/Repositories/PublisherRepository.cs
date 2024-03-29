using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly DataContext _ctx;
    public PublisherRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool PublisherExist(int PublisherId)
    {
        return _ctx.Publishers.Any(r => r.PublisherId == PublisherId);
    }
    public async Task<ICollection<Publisher>> GetAllPublishersAsync()
    {
        return await _ctx.Publishers.Include(a => a.User).ToListAsync();
    }
    public async Task<Publisher> GetPublisherByIdAsync(int id)
    {
        return await _ctx.Publishers.Include(a => a.User).FirstOrDefaultAsync(a => a.PublisherId == id);
    }
    public async Task<IEnumerable<Publisher>> GetPublishersByUserId(int userId)
    {
        return await _ctx.Publishers
        .Include(a => a.User)
        .Where(a => a.User.UserId == userId).ToListAsync();
    }
    public async Task<Publisher> AddPublisherAsync(Publisher publisher)
    {
        var user = await _ctx.Users.FindAsync(publisher.User.UserId);
        if (user == null)
        {
            return null;
        }
        var userAlreadyHere = await _ctx.Publishers.AnyAsync(p => p.User.UserId == publisher.User.UserId);
        if (userAlreadyHere)
        {
            return null;
        }
        publisher.User = user;

        _ctx.Publishers.Add(publisher);
        await _ctx.SaveChangesAsync();

        return publisher;
    }
    public async Task<Publisher> UpdatePublisherAsync(Publisher publisher)
    {
        var user = await _ctx.Users.FindAsync(publisher.User.UserId);
        if (user == null)
        {
            return null;
        }
        var userAlreadyHere = await _ctx.Publishers.AnyAsync(p => p.User.UserId == publisher.User.UserId);
        if (userAlreadyHere)
        {
            return null;
        }
        publisher.User = user;

        _ctx.Publishers.Update(publisher);
        await _ctx.SaveChangesAsync();

        return publisher;
    }
    public async Task DeletePublishersAsync(int publisherId)
    {
        var publisher = await _ctx.Publishers.FindAsync(publisherId);
        if (publisher == null)
        {
            return;
        }

        _ctx.Publishers.Remove(publisher);
        await _ctx.SaveChangesAsync();
        return;
    }
}
