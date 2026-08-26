namespace MyApi.Application.Dtos.RequestDtos
{
    public class UpdateEventRequest
    {
        public string? EventName { get; set; }

        public string? EventDescription { get; set; }

        public string? Eventvenue { get; set; }

        public DateTime? EventDate { get; set; }
    }
}