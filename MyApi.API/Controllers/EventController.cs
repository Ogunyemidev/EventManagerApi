
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Services.Interfaces;
using System.Security.Claims;

namespace MyApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventServices;

        public EventsController(IEventService eventServices)
        {
            _eventServices = eventServices;
        }


        // =========================================================
        // US-BE-001
        // Get Upcoming Events
        //
        // GET: api/events?pageNumber=1&pageSize=10
        // =========================================================

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUpcomingEvents(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var events = await _eventServices
                .GetUpcomingEventsAsync(
                    pageNumber,
                    pageSize);

            return Ok(events);
        }


        // =========================================================
        // US-BE-002
        // Search Events
        //
        // GET: api/events/search
        // =========================================================

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchEvents(
            [FromQuery] SearchEventRequest request)
        {
            var events = await _eventServices
                .SearchEventsAsync(request);

            return Ok(events);
        }


        // =========================================================
        // US-BE-003
        // Get Event By ID
        //
        // GET: api/events/{eventId}
        // =========================================================

        [HttpGet("{eventId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventById(
            Guid eventId)
        {
            var eventDetails = await _eventServices
                .GetEventByIdAsync(eventId);

            return Ok(eventDetails);
        }


        // =========================================================
        // US-BE-007
        // Create Event
        //
        // POST: api/events
        //
        // Organizer ID comes from JWT.
        // =========================================================

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent(
            [FromBody] CreateEventRequest request)
        {
            var organizerId = GetCurrentUserId();

            var createdEvent = await _eventServices
                .CreateEventAsync(
                    request,
                    organizerId);

            return CreatedAtAction(
                nameof(GetEventById),
                new
                {
                    eventId = createdEvent.EventId
                },
                createdEvent);
        }


        // =========================================================
        // Update Event
        //
        // PUT: api/events/{eventId}
        //
        // Only the organizer who owns the event can update it.
        // =========================================================

        [HttpPut("{eventId:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent(
            Guid eventId,
            [FromBody] UpdateEventRequest request)
        {
            var organizerId = GetCurrentUserId();

            await _eventServices.UpdateEventAsync(
                eventId,
                request,
                organizerId);

            return Ok(new
            {
                message = "Event updated successfully."
            });
        }


        // =========================================================
        // Cancel Event
        //
        // DELETE: api/events/{eventId}
        //
        // Only the organizer who owns the event can cancel it.
        // =========================================================

        [HttpDelete("{eventId:guid}")]
        [Authorize]
        public async Task<IActionResult> CancelEvent(
            Guid eventId)
        {
            var organizerId = GetCurrentUserId();

            await _eventServices.CancelEventAsync(
                eventId,
                organizerId);

            return Ok(new
            {
                message = "Event cancelled successfully."
            });
        }


        // =========================================================
        // Get Current User ID From JWT
        // =========================================================

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException(
                    "Invalid or missing user ID.");
            }

            return userId;
        }
    }
}
