using CourseworkDB.Data.Models;

namespace CourseworkDB.Data.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByDateAsync(DateTime date);
        Task<Payment> GetPaymentByGroupAsync(int GroupId);
        Task<Payment> GetPaymentByIdAsync(int Id);
        Task<ICollection<Payment>> GetPaymentsAsync();
        Task<ICollection<Payment>> GetPaymentsInDecreasingOrderAsync();
        bool PaymentExists(int Id);
    }
}