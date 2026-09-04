using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Application.Services.Interfaces
{
    public interface IPaymentGateway
    {
        
    Task<PaymentGatewayResponse> InitializePaymentAsync(
        decimal amount,
        string currency,
        string transactionId);

    Task<PaymentGatewayResponse> VerifyPaymentAsync(
        string gatewayTransactionId);

    }
}