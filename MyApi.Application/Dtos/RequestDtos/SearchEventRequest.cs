namespace MyApi.Application.Dtos.RequestDtos
{
    public class SearchEventRequest
    {
        public string? SearchTerm { get; set; }

        public string? Venue { get; set; }

        public DateTime? EventDate { get; set; }
    }
}