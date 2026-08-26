using Microsoft.EntityFrameworkCore;
using MyApi.Application.IRepositories;
using MyApi.Domain.Entities;
using MyApi.Infrastructure.Persistence;

namespace MyApi.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByIdAsync(Guid paymentId)
        {
            return await _context.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p => p.Id == paymentId);
        }

        public async Task<List<Payment>> GetByBookingIdAsync(
            Guid bookingId)
        {
            return await _context.Payments
                .Where(p => p.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<Payment?> GetByTransactionIdAsync(
            string transactionId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(
                    p => p.TransactionId == transactionId);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}