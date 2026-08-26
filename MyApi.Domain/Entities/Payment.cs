using MyApi.Domain.Enums;

namespace MyApi.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid BookingId { get; set; }

        public Booking Booking { get; set; } = default!;

        public decimal Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string TransactionId { get; set; }
            = string.Empty;
    }
}