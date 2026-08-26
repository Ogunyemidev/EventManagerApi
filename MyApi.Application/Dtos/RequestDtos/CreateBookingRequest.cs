namespace MyApi.Application.Dtos.RequestDtos
{
    public class CreateBookingRequest
    {
        public Guid EventId { get; set; }

        public List<CreateBookingItemRequest> Items { get; set; }
            = new();
    }

    public class CreateBookingItemRequest
    {
        public Guid TicketTypeId { get; set; }

        public int Quantity { get; set; }
    }
}