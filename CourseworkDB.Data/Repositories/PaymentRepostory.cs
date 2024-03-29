using CourseworkDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseworkDB.Data.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly DataContext _ctx;
    public PaymentRepository(DataContext ctx)
    {
        _ctx = ctx;
    }
    public bool PaymentExists(int Id)
    {
        return _ctx.Payments.Any(p => p.PaymentId == Id);
    }
    public async Task<ICollection<Payment>> GetPaymentsAsync()
    {
        return await _ctx.Payments.OrderBy(p => p.PaymentId).ToListAsync();
    }
    public async Task<Payment> GetPaymentByIdAsync(int Id)
    {
        return await _ctx.Payments.Include(p => p.AdGroup).FirstOrDefaultAsync(p => p.PaymentId == Id);
    }
    public async Task<Payment> GetPaymentByDateAsync(DateTime date)
    {
        return await _ctx.Payments.Include(p => p.AdGroup).FirstOrDefaultAsync(p => p.PaymentDate == date);
    }
    public async Task<Payment> GetPaymentByGroupAsync(int GroupId)
    {
        return await _ctx.Payments.Include(p => p.AdGroup).FirstOrDefaultAsync(p => p.AdGroup.GroupId == GroupId);
    }
    public async Task<ICollection<Payment>> GetPaymentsInDecreasingOrderAsync()
    {
        return await _ctx.Payments.OrderByDescending(p => p.PaymentAmount).ToListAsync();
    }
}
