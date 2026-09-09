using MyApi.Domain.Enums;

namespace MyApi.Domain.Entities
{
    public class Event : BaseEntity
    {
        public Guid OrganizerId { get; set; }

        public User Organizer { get; set; } = default!;

        public required string EventName { get; set; }

        public string? EventDescription { get; set; }

        public required string Eventvenue { get; set; }

        public DateTime EventDate { get; set; }

        public EventStatus EventStatus { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public ICollection<TicketType> TicketTypes { get; set; }
            = new List<TicketType>();
    }
}