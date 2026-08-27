using System;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Application.Dtos.RequestDtos
{
    public class BookingItemRequest
    {
        [Required]
        public Guid TicketTypeId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}