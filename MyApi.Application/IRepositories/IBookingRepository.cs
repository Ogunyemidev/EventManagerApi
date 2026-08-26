
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Domain.Entities;

namespace MyApi.Application.IRepositories
{
    public interface IBookingRepository
    {
        Task<bool> CreateBookingAsync(Booking booking);

        Task<Booking?> GetByIdAsync(Guid bookingId);

        Task<List<Booking>> GetAllAsync();

        Task<List<Booking>> GetByCustomerIdAsync(
            Guid customerId);

        Task<List<Booking>> GetByEventIdAsync(
            Guid eventId);

        Task<Booking?> GetByCustomerAndEventAsync(
            Guid customerId,
            Guid eventId);

        Task<bool> UpdateBookingAsync(
            Booking booking);

        Task<bool> DeleteBookingAsync(
            Guid bookingId);

        Task<bool> ExistsAsync(
            Guid bookingId);
    }
}