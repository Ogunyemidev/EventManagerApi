using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Domain.Entities;

namespace MyApi.Domain.Entities
{
    public class TicketType : BaseEntity
    {
        public Guid? TicketTypeId { get; set; }

        public Guid? EventId { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; } = default!;

        public int Capacity { get; set; } = default!;

        public int RemainingQuantity { get; set; } = default!;
    }
}