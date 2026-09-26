using MyApi.Application.Authentication;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Application.IRepositories;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;
using MyApi.Domain.Enums;

namespace MyApi.Application.Services.Implementations
{
    public class BookingServices : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentServices _paymentServices;
        private readonly ICurrentUser _currentUser;

        public BookingServices(
            IBookingRepository bookingRepository,
            IPaymentServices paymentServices,
            ICurrentUser currentUser)
        {
            _bookingRepository = bookingRepository;
            _paymentServices = paymentServices;
            _currentUser = currentUser;
        }

        public async Task<Booking> ReserveTicketsAsync(
            CreateBookingRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var booking = new Booking
            {
                CustomerId = _currentUser.LoggedInUserId(),
                EventId = request.EventId,
                BookingStatus = BookingStatus.Reserved,
                ReservedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(15)
            };

            await _bookingRepository.CreateBookingAsync(booking);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetMyBookingsAsync(
            CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetByCustomerIdAsync(
                _currentUser.LoggedInUserId());
        }

        public async Task<IEnumerable<Booking>> GetEventBookingsAsync(
            Guid eventId,
            CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetByEventIdAsync(eventId);
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(
            Guid bookingId,
            PaymentRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var booking = await _bookingRepository.GetByIdAsync(bookingId)
                ?? throw new KeyNotFoundException("Booking not found.");

            var paymentMethod = Enum.TryParse<PaymentMethod>(
                request.PaymentMethod,
                true,
                out var parsedMethod)
                ? parsedMethod
                : PaymentMethod.Card;

            return await _paymentServices.ProcessPaymentAsync(
                new ProcessPaymentRequest
                {
                    BookingId = bookingId,
                    Amount = booking.TotalAmount,
                    PaymentMethod = paymentMethod
                },
                _currentUser.LoggedInUserId());
        }

        public async Task<bool> CancelBookingAsync(
            Guid bookingId,
            CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                return false;

            booking.BookingStatus = BookingStatus.Cancelled;
            return await _bookingRepository.UpdateBookingAsync(booking);
        }

        public Task<IEnumerable<TicketDto>> GetBookingTicketsAsync(
            Guid bookingId,
            CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<TicketDto>>(
                Array.Empty<TicketDto>());
        }

        public async Task<BookingStatusDto> GetBookingStatusAsync(
            Guid bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId)
                ?? throw new KeyNotFoundException("Booking not found.");
            var isExpired = booking.BookingStatus == BookingStatus.Expired
                || booking.ExpiredAt <= DateTime.UtcNow;

            return new BookingStatusDto
            {
                CreatedBy = userId.ToString(),
                BookingId = booking.Id,
                Status = isExpired ? BookingStatus.Expired.ToString() : booking.BookingStatus.ToString(),
                ExpiresAt = booking.ExpiredAt,
                IsExpired = isExpired,
                TotalAmount = booking.TotalAmount
            };
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