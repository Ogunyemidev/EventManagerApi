using MyApi.Domain.Enums;

namespace MyApi.Application.Dtos.RequestDtos
{
    public class ProcessPaymentRequest
    {
        public Guid BookingId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}