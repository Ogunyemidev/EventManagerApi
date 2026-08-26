namespace MyApi.Application.Dtos.ResponseDtos
{
    public class TicketTypeDto
    {
        public Guid TicketTypeId { get; set; }

        public Guid EventId { get; set; }

        public string TicketTypeName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int AvailableQuantity { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}