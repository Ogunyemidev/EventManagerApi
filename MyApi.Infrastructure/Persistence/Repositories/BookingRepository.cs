using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByCustomerIdAsync(
            Guid customerId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByEventIdAsync(
            Guid eventId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .Where(b => b.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Booking?> GetByCustomerAndEventAsync(
            Guid customerId,
            Guid eventId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b =>
                    b.CustomerId == customerId &&
                    b.EventId == eventId);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public void Update(Booking booking)
        {
            _context.Bookings.Update(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            _context.Bookings.Remove(booking);
        }

        public async Task<bool> ExistsAsync(Guid bookingId)
        {
            return await _context.Bookings
                .AnyAsync(b => b.BookingId == bookingId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
    
