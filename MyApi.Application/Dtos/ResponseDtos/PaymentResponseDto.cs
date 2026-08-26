using System;
using MyApi.Domain.Enums;

namespace MyApi.Application.Dtos.ResponseDtos
{
    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }

        public Guid BookingId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string TransactionId { get; set; } = string.Empty;
    }
}