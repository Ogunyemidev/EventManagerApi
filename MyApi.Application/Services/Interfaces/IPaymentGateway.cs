using MyApi.Application.Dtos.ResponseDtos;

namespace MyApi.Application.Services.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentGatewayResponse> InitializePaymentAsync(
        decimal amount,
        string currency,
        string transactionId);

    Task<PaymentGatewayResponse> VerifyPaymentAsync(
        string gatewayTransactionId);
}