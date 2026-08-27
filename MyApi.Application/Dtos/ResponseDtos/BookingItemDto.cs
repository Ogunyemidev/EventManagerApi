using System;

namespace MyApi.Application.Dtos.ResponseDtos
{
    public class BookingItemDto
    {
        public Guid Id { get; set; }

        public Guid TicketTypeId { get; set; }

        public string? TicketTypeName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}