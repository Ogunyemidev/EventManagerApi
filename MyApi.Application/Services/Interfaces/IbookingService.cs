using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking?> GetBookingByIdAsync(Guid bookingId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();   

        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(Guid customerId);   

        Task<IEnumerable<Booking>> GetBookingsByEventIdAsync(Guid eventId); 

        Task<Booking?> GetBookingByCustomerAndEventAsync(Guid customerId, Guid eventId);    

        Task<bool> CreateBookingAsync(Booking booking);

        Task<bool> UpdateBookingAsync(Booking booking);

        Task<bool> DeleteBookingAsync(Guid bookingId);

        Task<bool> BookingExistsAsync(Guid bookingId);  
    }
}