using MyApi.Domain.Enums;

namespace MyApi.Application.Dtos.RequestDtos
{
    public class PaymentCallbackRequest
    {
        public string TransactionId { get; set; }
            = string.Empty;

        public PaymentStatus PaymentStatus { get; set; }
    }
}