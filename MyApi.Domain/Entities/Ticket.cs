using MyApi.Domain.Entities;
using MyApi.Domain.Enums;



namespace MyApi.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        

        public required Guid BookingId { get; set; }

        public Booking Booking { get; set; } = default!;

        public required Guid TicketTypeId { get; set; }

        public required TicketType TicketType { get; set; } = default!; 

        public string QRCode { get; set; } = default!;

        public required string HolderName { get; set; }

        public required TicketStatus TicketStatus { get; set; }


    }
}