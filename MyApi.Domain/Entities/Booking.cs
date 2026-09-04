using MyApi.Domain.Entities;
using MyApi.Domain.Enums;

namespace MyApi.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid CustomerId { get; set; }

        public Guid EventId { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public DateTime ReservedAt { get; set; }

        public DateTime ExpiredAt { get; set; }

        public decimal TotalAmount { get; set; }

        public User User { get; set; } = default!;

        public Event Event { get; set; } = default!;

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}