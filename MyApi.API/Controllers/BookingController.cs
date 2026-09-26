using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Services.Interfaces;

namespace MyApi.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Reserve tickets for an event.
        /// </summary>
        [HttpPost("reserve")]
        [Authorize(Roles = "Organizer, Customer")]
        public async Task<IActionResult> ReserveTickets(
            [FromBody] CreateBookingRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _bookingService.ReserveTicketsAsync(
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetBookingById),
                new { id = response.Id },
                response);
        }

        /// <summary>
        /// Get a booking by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            var response = await _bookingService.GetBookingByIdAsync(id);

            if (response == null)
            {
                return NotFound(new
                {
                    message = $"Booking with ID {id} was not found."
                });
            }

            return Ok(response);
        }

        /// <summary>
        /// Get the currently authenticated customer's bookings.
        /// </summary>
        [HttpGet("my-bookings")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyBookings(
            CancellationToken cancellationToken)
        {
            var response = await _bookingService.GetMyBookingsAsync(
                cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Get bookings for a specific event.
        /// Organizers can use this to monitor bookings for their events.
        /// </summary>
        [HttpGet("event/{eventId:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetEventBookings(
            Guid eventId,
            CancellationToken cancellationToken)
        {
            var response = await _bookingService.GetEventBookingsAsync(
                eventId,
                cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Confirm payment for a booking.
        /// </summary>
        [HttpPost("{bookingId:guid}/payment")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ProcessPayment(
            Guid bookingId,
            [FromBody] PaymentRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _bookingService.ProcessPaymentAsync(
                bookingId,
                request,
                cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Cancel a booking.
        /// </summary>
        [HttpPost("{bookingId:guid}/cancel")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelBooking(
            Guid bookingId,
            CancellationToken cancellationToken)
        {
            var response = await _bookingService.CancelBookingAsync(
                bookingId,
                cancellationToken);

            return Ok(new
            {
                success = response,
                message = response
                    ? "Booking cancelled successfully."
                    : "Booking could not be cancelled."
            });
        }

        /// <summary>
        /// Get all tickets generated for a confirmed booking.
        /// </summary>
        [HttpGet("{bookingId:guid}/tickets")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetBookingTickets(
            Guid bookingId,
            CancellationToken cancellationToken)
        {
            var response = await _bookingService.GetBookingTicketsAsync(
                bookingId,
                cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Check whether a booking has expired.
        /// </summary>
        [HttpGet("{bookingId:guid}/status")]
        public async Task<IActionResult> GetBookingStatus(Guid bookingId)
        {
            var response = await _bookingService.GetBookingStatusAsync(
                bookingId);

            return Ok(response);
        }
    }
}

