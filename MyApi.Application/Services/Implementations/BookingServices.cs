
using MyApi.Application.IRepositories;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Implementations
{
    public class BookingServices : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingServices(
            IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }


        public async Task<Booking?> GetBookingByIdAsync(
            Guid bookingId)
        {
            return await _bookingRepository
                .GetByIdAsync(bookingId);
        }


        public async Task<IEnumerable<Booking>>
            GetAllBookingsAsync()
        {
            return await _bookingRepository
                .GetAllAsync();
        }


        public async Task<IEnumerable<Booking>>
            GetBookingsByCustomerIdAsync(
                Guid customerId)
        {
            return await _bookingRepository
                .GetByCustomerIdAsync(customerId);
        }


        public async Task<IEnumerable<Booking>>
            GetBookingsByEventIdAsync(
                Guid eventId)
        {
            return await _bookingRepository
                .GetByEventIdAsync(eventId);
        }


        public async Task<Booking?>
            GetBookingByCustomerAndEventAsync(
                Guid customerId,
                Guid eventId)
        {
            return await _bookingRepository
                .GetByCustomerAndEventAsync(
                    customerId,
                    eventId);
        }


        public async Task<bool> CreateBookingAsync(
            Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(
                    nameof(booking));

            return await _bookingRepository
                .CreateBookingAsync(booking);
        }


        public async Task<bool> UpdateBookingAsync(
            Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(
                    nameof(booking));

            return await _bookingRepository
                .UpdateBookingAsync(booking);
        }


        public async Task<bool> DeleteBookingAsync(
            Guid bookingId)
        {
            if (bookingId == Guid.Empty)
                return false;

            var booking = await _bookingRepository
                .GetByIdAsync(bookingId);

            if (booking == null)
                return false;

            return await _bookingRepository
                .DeleteBookingAsync(bookingId);
        }


        public async Task<bool> BookingExistsAsync(
            Guid bookingId)
        {
            if (bookingId == Guid.Empty)
                return false;

            return await _bookingRepository
                .ExistsAsync(bookingId);
        }
    }
}