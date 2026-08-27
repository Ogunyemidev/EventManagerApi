using System;
using System.Collections.Generic;

namespace MyApi.Application.Dtos.ResponseDtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid EventId { get; set; }

        public string? EventName { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime ReservedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public decimal TotalAmount { get; set; }

        public List<BookingItemDto> Items { get; set; } = new();

        public List<TicketDto> Tickets { get; set; } = new();
    }
}