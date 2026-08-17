using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Application.IRepositories;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Implementations
{
    public class BookingServices : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingServices(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid bookingId)
        {
            return await _bookingRepository.GetByIdAsync(bookingId);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(Guid customerId)
        {
            return await _bookingRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByEventIdAsync(Guid eventId)
        {
            return await _bookingRepository.GetByEventIdAsync(eventId);
        }

        public async Task<Booking?> GetBookingByCustomerAndEventAsync(Guid customerId, Guid eventId)
        {
            return await _bookingRepository.GetByCustomerAndEventAsync(customerId, eventId);
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            return await _bookingRepository.CreateBooking(booking);
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            return await _bookingRepository.UpdateBookingAsync(booking);
        }

        public async Task<bool> DeleteBookingAsync(Guid bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return false;
            }

            return await _bookingRepository.DeleteBooking(bookingId);
        }

        public async Task<bool> BookingExistsAsync(Guid bookingId)
        {
            return await _bookingRepository.ExistsAsync(bookingId);
        }
    }
}