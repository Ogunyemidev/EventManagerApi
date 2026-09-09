namespace MyApi.Application.Dtos.RequestDtos
{
    public class CreateEventRequest
    {
        public required string EventName { get; set; }

        public string? EventDescription { get; set; }

        public required string Eventvenue { get; set; }

        public DateTime EventDate { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }
    }
}