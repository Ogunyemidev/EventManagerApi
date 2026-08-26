namespace MyApi.Application.Dtos.RequestDtos
{
    public class UpdateTicketTypeRequest
    {
        public string? TicketTypeName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }
    }
}