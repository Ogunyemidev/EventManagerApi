using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Infrastructure.Payments
{
    public class PaypalPaymentGateway : IPaymentGateway
    
{
    private readonly HttpClient _httpClient;
    private readonly PayPalOptions _options;

    public PayPalPaymentGateway(
        HttpClient httpClient,
        IOptions<PayPalOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<PaymentGatewayResponse>
        InitializePaymentAsync(
            decimal amount,
            string currency,
            string transactionId)
    {
        // Create PayPal order
        // Return approval URL

        throw new NotImplementedException();
    }

    public async Task<PaymentGatewayResponse>
        VerifyPaymentAsync(string gatewayTransactionId)
    {
        // Ask PayPal to verify the order/payment

        throw new NotImplementedException();
    }
}
    }
