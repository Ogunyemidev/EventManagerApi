namespace MyApi.Domain.Entities
{
    public class BookingItem : BaseEntity
    {
        public Guid BookingItemId { get; set; }

        public Guid BookingId { get; set; }

        public Booking Booking { get; set; } = default!;

        public Guid TicketTypeId { get; set; }

        public TicketType TicketType { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}