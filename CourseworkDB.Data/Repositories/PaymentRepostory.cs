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
    public async Task<Payment> AddPaymentAsync(Payment payment)
    {
        var adGroups = await _ctx.AdGroups.FindAsync(payment.AdGroup.GroupId);
        if (adGroups == null)
        {
            return null;
        }
        var adGroupsAlreadyHere = await _ctx.Payments.AnyAsync(p => p.AdGroup.GroupId == payment.AdGroup.GroupId);
        if (adGroupsAlreadyHere)
        {
            return null;
        }
        payment.AdGroup = adGroups;

        _ctx.Payments.Add(payment);
        await _ctx.SaveChangesAsync();

        return payment;
    }
    public async Task<Payment> UpdatePaymentAsync(Payment payment)
    {
        var adGroups = await _ctx.AdGroups.FindAsync(payment.AdGroup.GroupId);
        if (adGroups == null)
        {
            return null;
        }
        payment.AdGroup = adGroups;

        _ctx.Payments.Update(payment);
        await _ctx.SaveChangesAsync();

        return payment;
    }
    public async Task DeletePaymentsAsync(int paymentId)
    {
        var payment = await _ctx.Payments.FindAsync(paymentId);
        if (payment == null)
        {
            return;
        }

        _ctx.Payments.Remove(payment);
        await _ctx.SaveChangesAsync();
        return;
    }
}
