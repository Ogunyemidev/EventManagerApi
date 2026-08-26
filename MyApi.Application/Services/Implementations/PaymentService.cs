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

        public PaymentServices(
            IPaymentRepository paymentRepository,
            IBookingRepository bookingRepository)
        {
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(
            ProcessPaymentRequest request,
            Guid userId)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.BookingId == Guid.Empty)
                throw new ArgumentException("Booking ID is required.");

            if (request.Amount <= 0)
                throw new ArgumentException(
                    "Payment amount must be greater than zero.");

            // Check that booking exists
            var booking = await _bookingRepository
                .GetByIdAsync(request.BookingId);

            if (booking == null)
                throw new KeyNotFoundException(
                    "Booking not found.");

            // Check whether this booking already has a successful payment
            var existingPayments = await _paymentRepository
                .GetByBookingIdAsync(request.BookingId);

            var successfulPayment = existingPayments
                .FirstOrDefault(x =>
                    x.PaymentStatus == PaymentStatus.Succeeded);

            if (successfulPayment != null)
            {
                return MapToResponseDto(successfulPayment);
            }

            // Generate transaction ID
            var transactionId =
                $"PAY-{Guid.NewGuid():N}".ToUpperInvariant();

            var payment = new Payment
            {
                BookingId = request.BookingId,
                Amount = request.Amount,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = request.PaymentMethod,
                TransactionId = transactionId
            };

            await _paymentRepository.AddAsync(payment);

            await _paymentRepository.SaveChangesAsync();

            return MapToResponseDto(payment);
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