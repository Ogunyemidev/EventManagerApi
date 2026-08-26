using MyApi.Domain.Entities;
using MyApi.Domain.Enums;



public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid paymentId);

    Task<List<Payment>> GetByBookingIdAsync(Guid bookingId);

    Task<Payment?> GetByTransactionIdAsync(string transactionId);

    Task AddAsync(Payment payment);

    Task SaveChangesAsync();
}