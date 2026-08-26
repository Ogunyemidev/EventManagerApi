namespace MyApi.Application.Dtos.RequestDtos
{
    public class CreateEventRequest
    {
        public string EventName { get; set; } = string.Empty;

        public string? EventDescription { get; set; }

        public string Eventvenue { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }
    }
}