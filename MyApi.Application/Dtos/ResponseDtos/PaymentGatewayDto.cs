using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Application.Dtos.ResponseDtos
{
    public class PaymentGatewayDto
    {
        public bool Success { get; set; }

    public string TransactionId { get; set; } = string.Empty;

    public string PaymentUrl { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
    }
}