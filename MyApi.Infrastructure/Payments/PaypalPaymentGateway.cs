using Microsoft.Extensions.Options;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Application.Services.Interfaces;

namespace MyApi.Infrastructure.Payments;

public class PaypalPaymentGateway : IPaymentGateway
{
    private readonly HttpClient _httpClient;
    private readonly PayPalOptions _options;

    public PaypalPaymentGateway(
        HttpClient httpClient,
        IOptions<PayPalOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<PaymentGatewayResponse> InitializePaymentAsync(
        decimal amount,
        string currency,
        string transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaymentGatewayResponse> VerifyPaymentAsync(
        string gatewayTransactionId)
    {
        throw new NotImplementedException();
    }
}