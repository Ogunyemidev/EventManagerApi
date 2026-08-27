
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> ReserveTicketsAsync(
            CreateBookingRequest request,
            CancellationToken cancellationToken);

        Task<IEnumerable<Booking>> GetMyBookingsAsync(
            CancellationToken cancellationToken);

        Task<IEnumerable<Booking>> GetEventBookingsAsync(
            Guid eventId,
            CancellationToken cancellationToken);

        Task<PaymentResponseDto> ProcessPaymentAsync(
            Guid bookingId,
            PaymentRequest request,
            CancellationToken cancellationToken);

        Task<bool> CancelBookingAsync(
            Guid bookingId,
            CancellationToken cancellationToken);

        Task<IEnumerable<TicketDto>> GetBookingTicketsAsync(
            Guid bookingId,
            CancellationToken cancellationToken);

        Task<BookingStatusDto> GetBookingStatusAsync(
            Guid bookingId);

        Task<Booking?> GetBookingByIdAsync(
            Guid bookingId);

        Task<IEnumerable<Booking>> GetAllBookingsAsync();

        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(
            Guid customerId);

        Task<IEnumerable<Booking>> GetBookingsByEventIdAsync(
            Guid eventId);

        Task<Booking?> GetBookingByCustomerAndEventAsync(
            Guid customerId,
            Guid eventId);

        Task<bool> CreateBookingAsync(
            Booking booking);

        Task<bool> UpdateBookingAsync(
            Booking booking);

        Task<bool> DeleteBookingAsync(
            Guid bookingId);

        Task<bool> BookingExistsAsync(
            Guid bookingId);
    }
}
