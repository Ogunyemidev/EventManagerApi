namespace MyApi.Application.Dtos.ResponseDtos
{
    public class TicketValidationResponse
    {
        public bool IsValid { get; set; }

        public string Message { get; set; } = string.Empty;

        public Guid? TicketId { get; set; }

        public Guid? BookingId { get; set; }

        public Guid? EventId { get; set; }

        public string? TicketCode { get; set; }

        public string? EventName { get; set; }

        public string? TicketTypeName { get; set; }
    }
}