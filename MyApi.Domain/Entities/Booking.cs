using MyApi.Domain.Enums;


namespace MyApi.Domain.Entities
{
    public class Booking
    {
        public Guid? BookingId { get; set; }

        public required Guid CustomerId { get; set; }

        public required Guid EventId { get; set; }

        public required Event Event { get; set; }

        public required User User { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;


        public decimal TotalAmount { get; set; } = default!;
    }
}