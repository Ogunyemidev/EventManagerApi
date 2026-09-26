using System;
using MyApi.Domain.Enums;

namespace MyApi.Application.Dtos.ResponseDtos
{
    public class EventDetailsDto
    {
        public Guid EventId { get; set; }

        public Guid OrganizerId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string? EventDescription { get; set; }

        public string Eventvenue { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public EventStatus EventStatus { get; set; }
        public decimal Price { get; set; }

        public string? Image { get; set; }
    }
}