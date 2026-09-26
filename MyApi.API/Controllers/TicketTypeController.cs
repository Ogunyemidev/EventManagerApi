using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Services.Interfaces;

namespace MyApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeServices _ticketTypeServices;

        public TicketTypeController(
            ITicketTypeServices ticketTypeServices)
        {
            _ticketTypeServices = ticketTypeServices;
        }


        // ============================================================
        // CREATE TICKET TYPE
        // POST: api/TicketType
        // ============================================================
        [HttpPost]
        [Authorize(Roles = "Organizer,Admin,SuperAdmin")]
        public async Task<IActionResult> CreateTicketType(
            [FromBody] CreateTicketTypeRequest request)
        {
            try
            {
                var organizerId = GetUserId();

                var result =
                    await _ticketTypeServices.CreateTicketTypeAsync(
                        request,
                        organizerId);

                return CreatedAtAction(
                    nameof(GetTicketTypeById),
                    new { ticketTypeId = result.TicketTypeId },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    statusCode = 401,
                    message = ex.Message
                });
            }
        }


        // ============================================================
        // GET ALL TICKET TYPES FOR AN EVENT

        [HttpGet("all")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAllTicketTypes()
        {
            try
            {
                var result =
                    await _ticketTypeServices.GetAllTicketTypesAsync();

                return Ok(new
                {
                    statusCode = 200,
                    message = "All ticket types retrieved successfully.",
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
        }
        // GET: api/TicketType/event/{eventId}
        // ============================================================
        [HttpGet("event/{eventId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTicketTypesByEventId(
            Guid eventId)
        {
            try
            {
                var result =
                    await _ticketTypeServices
                        .GetTicketTypesByEventIdAsync(eventId);

                return Ok(new
                {
                    statusCode = 200,
                    message = "Ticket types retrieved successfully.",
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
        }


        // ============================================================
        // GET SINGLE TICKET TYPE
        // GET: api/TicketType/{ticketTypeId}
        // ============================================================
        [HttpGet("{ticketTypeId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTicketTypeById(
            Guid ticketTypeId)
        {
            try
            {
                var result =
                    await _ticketTypeServices
                        .GetTicketTypeByIdAsync(ticketTypeId);

                return Ok(new
                {
                    statusCode = 200,
                    message = "Ticket type retrieved successfully.",
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = ex.Message
                });
            }
        }


        // ============================================================
        // UPDATE TICKET TYPE
        // PUT: api/TicketType/{ticketTypeId}
        // ============================================================
        [HttpPut("{ticketTypeId:guid}")]
        [Authorize(Roles = "Organizer,Admin,SuperAdmin")]
        public async Task<IActionResult> UpdateTicketType(
            Guid ticketTypeId,
            [FromBody] UpdateTicketTypeRequest request)
        {
            try
            {
                var organizerId = GetUserId();

                var result =
                    await _ticketTypeServices
                        .UpdateTicketTypeAsync(
                            ticketTypeId,
                            request,
                            organizerId);

                if (!result)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Ticket type not found."
                    });
                }

                return Ok(new
                {
                    statusCode = 200,
                    message = "Ticket type updated successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    statusCode = 409,
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    statusCode = 401,
                    message = ex.Message
                });
            }
        }


        // ============================================================
        // DELETE TICKET TYPE
        // DELETE: api/TicketType/{ticketTypeId}
        // ============================================================
        [HttpDelete("{ticketTypeId:guid}")]
        [Authorize(Roles = "Organizer,Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteTicketType(
            Guid ticketTypeId)
        {
            try
            {
                var organizerId = GetUserId();

                var result =
                    await _ticketTypeServices
                        .DeleteTicketTypeAsync(
                            ticketTypeId,
                            organizerId);

                if (!result)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Ticket type not found."
                    });
                }

                return Ok(new
                {
                    statusCode = 200,
                    message = "Ticket type deleted successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    statusCode = 409,
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    statusCode = 401,
                    message = ex.Message
                });
            }
        }


        // ============================================================
        // GET CURRENT USER ID FROM JWT
        // ============================================================
        private Guid GetUserId()
        {
            var userIdClaim =
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("sub");

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                throw new UnauthorizedAccessException(
                    "User ID was not found in the authentication token.");
            }

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException(
                    "Invalid user ID in authentication token.");
            }

            return userId;
        }
    }
}
