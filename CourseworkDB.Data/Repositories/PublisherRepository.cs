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
    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
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
}
