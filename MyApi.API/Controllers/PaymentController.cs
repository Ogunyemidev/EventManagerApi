using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Services.Interfaces;
using System.Security.Claims;

namespace MyApi.API.Controllers
{
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
private readonly IPaymentServices _paymentServices;


    public PaymentController(IPaymentServices paymentServices)
    {
        _paymentServices = paymentServices;
    }

    /// <summary>
    /// Initialize/process a payment for a booking.
    /// </summary>
    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment(
        [FromBody] ProcessPaymentRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                return Unauthorized(new
                {
                    message = "User ID could not be determined from the token."
                });
            }

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new
                {
                    message = "Invalid user ID in authentication token."
                });
            }

            var result = await _paymentServices.ProcessPaymentAsync(
                request,
                userId);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get payment by payment ID.
    /// </summary>
    [HttpGet("{paymentId:guid}")]
    public async Task<IActionResult> GetPaymentById(Guid paymentId)
    {
        try
        {
            var result = await _paymentServices
                .GetPaymentByIdAsync(paymentId);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get all payments associated with a booking.
    /// </summary>
    [HttpGet("booking/{bookingId:guid}")]
    public async Task<IActionResult> GetPaymentsByBookingId(Guid bookingId)
    {
        try
        {
            var result = await _paymentServices
                .GetPaymentsByBookingIdAsync(bookingId);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    /// <summary>
    /// Handles payment gateway callback/webhook.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("callback")]
    public async Task<IActionResult> PaymentCallback(
        [FromBody] PaymentCallbackRequest request)
    {
        var success = await _paymentServices
            .HandlePaymentCallbackAsync(request);

        if (!success)
        {
            return BadRequest(new
            {
                message = "Payment callback could not be processed."
            });
        }

        return Ok(new
        {
            message = "Payment callback processed successfully."
        });
    }
}

}
