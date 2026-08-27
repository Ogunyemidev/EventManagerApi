using System;

namespace MyApi.Application.Dtos.ResponseDtos
{
    public class BookingStatusDto
    {
        public Guid BookingId { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? ExpiresAt { get; set; }

        public bool IsExpired { get; set; }

        public decimal TotalAmount { get; set; }
    }
}