
using MyApi.Domain.Entities;
using MyApi.Domain.Enums;


namespace MyApi.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid? PaymentId { get; set; }

        public Guid? BookingId { get; set; }

        public decimal Amount { get; set; } = default!;

        public PaymentStatus PaymentStatus { get; set; } = default!;

        public PaymentMethod PaymentMethod { get; set; } = default!;

        public string TransactionId { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}