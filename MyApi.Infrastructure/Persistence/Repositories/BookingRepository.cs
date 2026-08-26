
using Microsoft.EntityFrameworkCore;
using MyApi.Application.IRepositories;
using MyApi.Domain.Entities;

namespace MyApi.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // Create Booking
        // =========================================================

        public async Task<bool> CreateBookingAsync(
            Booking booking)
        {
            await _context.Bookings.AddAsync(booking);

            return await _context.SaveChangesAsync() > 0;
        }


        // =========================================================
        // Get Booking By ID
        // =========================================================

        public async Task<Booking?> GetByIdAsync(
            Guid bookingId)
        {
            return await _context.Bookings

                .Include(b => b.Event)

                .Include(b => b.User)

                .Include(b => b.Payments)

                .FirstOrDefaultAsync(
                    b => b.Id == bookingId);
        }


        // =========================================================
        // Get All Bookings
        // =========================================================

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings

                .Include(b => b.Event)

                .Include(b => b.User)

                .Include(b => b.Payments)

                .Where(b => !b.IsDeleted)

                .ToListAsync();
        }


        // =========================================================
        // Get Bookings By Customer
        // =========================================================

        public async Task<List<Booking>> GetByCustomerIdAsync(
            Guid customerId)
        {
            return await _context.Bookings

                .Include(b => b.Event)

                .Include(b => b.User)

                .Include(b => b.Payments)

                .Where(b =>
                    b.CustomerId == customerId &&
                    !b.IsDeleted)

                .ToListAsync();
        }


        // =========================================================
        // Get Bookings By Event
        // =========================================================

        public async Task<List<Booking>> GetByEventIdAsync(
            Guid eventId)
        {
            return await _context.Bookings

                .Include(b => b.Event)

                .Include(b => b.User)

                .Include(b => b.Payments)

                .Where(b =>
                    b.EventId == eventId &&
                    !b.IsDeleted)

                .ToListAsync();
        }


        // =========================================================
        // Get Customer Booking For Event
        // =========================================================

        public async Task<Booking?> GetByCustomerAndEventAsync(
            Guid customerId,
            Guid eventId)
        {
            return await _context.Bookings

                .Include(b => b.Event)

                .Include(b => b.User)

                .Include(b => b.Payments)

                .FirstOrDefaultAsync(
                    b =>
                        b.CustomerId == customerId &&
                        b.EventId == eventId &&
                        !b.IsDeleted);
        }


        // =========================================================
        // Update Booking
        // =========================================================

        public async Task<bool> UpdateBookingAsync(
            Booking booking)
        {
            _context.Bookings.Update(booking);

            return await _context.SaveChangesAsync() > 0;
        }


        // =========================================================
        // Delete Booking
        // =========================================================

        public async Task<bool> DeleteBookingAsync(
            Guid bookingId)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(
                    b => b.Id == bookingId);

            if (booking == null)
            {
                return false;
            }

            // Soft delete
            booking.IsDeleted = true;
            booking.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }


        // =========================================================
        // Check Booking Exists
        // =========================================================

        public async Task<bool> ExistsAsync(
            Guid bookingId)
        {
            return await _context.Bookings
                .AnyAsync(
                    b =>
                        b.Id == bookingId &&
                        !b.IsDeleted);
        }
    }
}
