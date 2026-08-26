using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;

namespace MyApi.Application.Services.Interfaces
{
    public interface IPaymentServices
    {
        // Initialize/process payment
        Task<PaymentResponseDto> ProcessPaymentAsync(
            ProcessPaymentRequest request,
            Guid userId);

        // Get payment by ID
        Task<PaymentDto> GetPaymentByIdAsync(
            Guid paymentId);

        // Get payment by booking
        Task<List<PaymentDto>> GetPaymentsByBookingIdAsync(
            Guid bookingId);

        // Payment gateway callback/webhook
        Task<bool> HandlePaymentCallbackAsync(
            PaymentCallbackRequest request);
    }
}