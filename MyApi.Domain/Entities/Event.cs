using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Domain.BaseEntities;
using MyApi.Domain.Enums;

namespace MyApi.Domain.Entities
{
    public class Event: BaseEntity
    {
        public Guid? EventId { get; set; }

        public Guid? OrganizerId { get; set; }

        public required string EventName { get; set; }

        public string? EventDescription { get; set; }

        public required string Eventvenue { get; set; }

        public DateTime EventDate { get; set; }

        public EventStatus EventStatus { get; set; }

    }
}