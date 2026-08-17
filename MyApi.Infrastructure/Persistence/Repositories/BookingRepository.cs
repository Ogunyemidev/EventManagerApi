using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> CreateBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByEventIdAsync(Guid eventId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .Where(b => b.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Booking?> GetByCustomerAndEventAsync(Guid customerId, Guid eventId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.CustomerId == customerId && b.EventId == eventId);
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBooking(Guid id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Guid bookingId)
        {
            return await _context.Bookings.AnyAsync(b => b.BookingId == bookingId);
        }
    }
}

