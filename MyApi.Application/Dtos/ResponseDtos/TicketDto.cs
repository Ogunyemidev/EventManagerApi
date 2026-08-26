using MyApi.Domain.Enums;

namespace MyApi.Application.Dtos.ResponseDtos
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }

        public Guid EventId { get; set; }

        public Guid TicketTypeId { get; set; }

        public Guid CustomerId { get; set; }

        public decimal Amount { get; set; }

        public string? TicketCode { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}