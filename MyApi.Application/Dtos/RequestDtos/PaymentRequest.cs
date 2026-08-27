using System;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Application.Dtos.RequestDtos
{
    public class PaymentRequest
    {
        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = "Mock";

        public string? TransactionReference { get; set; }
    }
}