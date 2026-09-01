namespace MyApi.Domain.Entities
{
    public class TicketType : BaseEntity
    {
        public Guid TicketTypeId { get; set; }

        public Guid EventId { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public int RemainingQuantity { get; set; }
    }
}