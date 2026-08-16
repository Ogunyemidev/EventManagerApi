using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Application.IRepositories
{
    public interface IBookingRepository
    {
        Task<bool> CreateBooking(Booking booking);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<List<Booking>> GetAllAsync();

        Task<List<Booking>> GetByCustomerIdAsync(Guid customerId);
        Task<List<Booking>> GetByEventIdAsync(Guid eventId);

        Task<Booking?> GetByCustomerAndEventAsync(Guid customerId, Guid eventId);
        Task<bool> UpdateBookingAsync(Booking booking);

        Task<Booking> update(Booking booking);
        Task<bool> DeleteBooking(Guid id);
        Task<bool> ExistsAsync(Guid bookingId);
        Task<bool> SavedChangesAsync(Booking booking);
    }
}