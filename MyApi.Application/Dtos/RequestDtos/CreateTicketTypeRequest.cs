namespace MyApi.Application.Dtos.RequestDtos
{
    public class CreateTicketTypeRequest
    {
        public Guid EventId { get; set; }

        public string TicketTypeName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}