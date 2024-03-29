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
    public async Task<AdType> CreateAdTypeAsync(AdType adType)
    {
        _ctx.AdTypes.Add(adType);
        await _ctx.SaveChangesAsync();
        return adType;
    }
    public async Task<AdType> UpdateAdTypeAsync(AdType adType)
    {
        _ctx.AdTypes.Update(adType);
        await _ctx.SaveChangesAsync();
        return adType;
    }
    public async Task DeleteAdTypeAsync(int id)
    {
        var adType = await _ctx.AdTypes.FindAsync(id);
        if (adType == null)
        {
            return;
        }

        _ctx.AdTypes.Remove(adType);
        await _ctx.SaveChangesAsync();
    }
}
