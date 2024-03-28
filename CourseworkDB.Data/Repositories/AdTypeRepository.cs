using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseworkDB.Data.Repositories;

public class AdTypeRepository : IAdTypeRepository
{
    private readonly DataContext _ctx;
    public AdTypeRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool AdTypeExists(int id)
    {
        return _ctx.AdTypes.Any(r => r.TypeId == id);
    }
    public async Task<ICollection<AdType>> GetAdTypesAsync()
    {
        return await _ctx.AdTypes.OrderBy(r => r.TypeId).ToListAsync();
    }
    public async Task<AdType> GetAdTypeAsync(int id)
    {
        return await _ctx.AdTypes.FindAsync(id);
    }
    public async Task<AdType> GetAdTypeByNameAsync(string typename)
    {
        return await _ctx.AdTypes.AsNoTracking().FirstOrDefaultAsync(r => r.TypeName == typename);
    }
    //Ads by ad type
}
