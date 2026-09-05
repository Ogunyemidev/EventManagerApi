using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Application.IRepositories;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;
using MyApi.Domain.Enums;

namespace MyApi.Application.Services.Implementations
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentGateway _paymentGateway;

        public PaymentServices(
            IPaymentRepository paymentRepository,
            IBookingRepository bookingRepository,
            IPaymentGateway paymentGateway)
        {
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            _paymentGateway = paymentGateway;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(
            ProcessPaymentRequest request,
            Guid userId)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.BookingId == Guid.Empty)
                throw new ArgumentException("Booking ID is required.");

          
            // 1. Get booking
            var booking = await _bookingRepository
                .GetByIdAsync(request.BookingId);

            if (booking == null)
                throw new KeyNotFoundException(
                    "Booking not found.");

            // 2. Check that the booking belongs to the logged-in user
            if (booking.CustomerId != userId)
                throw new UnauthorizedAccessException(
                    "You are not authorized to pay for this booking.");

            // 3. Check whether booking already has successful payment
            var existingPayments = await _paymentRepository
                .GetByBookingIdAsync(request.BookingId);

            var successfulPayment = existingPayments
                .FirstOrDefault(x =>
                    x.PaymentStatus == PaymentStatus.Succeeded);

            if (successfulPayment != null)
            {
                return MapToResponseDto(successfulPayment);
            }

            // 4. Generate internal transaction ID
            var transactionId =
                $"PAY-{Guid.NewGuid():N}".ToUpperInvariant();

            // 5. Create pending payment
            var payment = new Payment
            {
                BookingId = request.BookingId,

                // Ideally use booking.TotalAmount instead of trusting
                // the amount sent by the frontend.
                Amount = booking.TotalAmount,

                PaymentStatus = PaymentStatus.Pending,

                PaymentMethod = request.PaymentMethod,

                TransactionId = transactionId
            };

            // 6. Save payment first
            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            // 7. Initialize payment with PayPal
            var gatewayResponse =
                await _paymentGateway.InitializePaymentAsync(
                    payment.Amount,
                    "NGN",
                    payment.TransactionId);

            // 8. Check whether PayPal initialization succeeded
            if (!gatewayResponse.Success)
            {
                payment.PaymentStatus = PaymentStatus.Failed;

                await _paymentRepository.SaveChangesAsync();

                throw new InvalidOperationException(
                    gatewayResponse.Message);
            }

            // 9. Return payment information + PayPal URL
            var response = MapToResponseDto(payment);

            response.PaymentUrl = gatewayResponse.PaymentUrl;
            response.GatewayTransactionId =
                gatewayResponse.TransactionId;

            return response;
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(
            Guid paymentId)
        {
            if (paymentId == Guid.Empty)
                throw new ArgumentException(
                    "Payment ID is required.");

            var payment = await _paymentRepository
                .GetByIdAsync(paymentId);

            if (payment == null)
                throw new KeyNotFoundException(
                    "Payment not found.");

            return MapToDto(payment);
        }

        public async Task<List<PaymentDto>> GetPaymentsByBookingIdAsync(
            Guid bookingId)
        {
            if (bookingId == Guid.Empty)
                throw new ArgumentException(
                    "Booking ID is required.");

            var payments = await _paymentRepository
                .GetByBookingIdAsync(bookingId);

            return payments
                .Select(MapToDto)
                .ToList();
        }

        public async Task<bool> HandlePaymentCallbackAsync(
            PaymentCallbackRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.TransactionId))
                return false;

            var payment = await _paymentRepository
                .GetByTransactionIdAsync(request.TransactionId);

            if (payment == null)
                return false;

            // Don't change an already successful payment
            if (payment.PaymentStatus == PaymentStatus.Succeeded)
                return true;

            payment.PaymentStatus = request.PaymentStatus;

            await _paymentRepository.SaveChangesAsync();

            return payment.PaymentStatus == PaymentStatus.Succeeded;
        }

        private static PaymentDto MapToDto(Payment payment)
        {
            return new PaymentDto
            {
                PaymentId = payment.Id,
                BookingId = payment.BookingId,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                CreatedAt = payment.CreatedDate
            };
        }

        private static PaymentResponseDto MapToResponseDto(
            Payment payment)
        {
            return new PaymentResponseDto
            {
                PaymentId = payment.Id,
                BookingId = payment.BookingId,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId
            };
        }
    }
}