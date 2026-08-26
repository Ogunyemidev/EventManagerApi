using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;

namespace MyApi.Application.Services.Interfaces
{
    public interface ITicketTypeServices
    {
        // Create ticket type for an event
        Task<TicketTypeDto> CreateTicketTypeAsync(
            CreateTicketTypeRequest request,
            Guid organizerId);

        // Get ticket types belonging to an event
        Task<List<TicketTypeDto>> GetTicketTypesByEventIdAsync(
            Guid eventId);

        // Get a specific ticket type
        Task<TicketTypeDto> GetTicketTypeByIdAsync(
            Guid ticketTypeId);

        // Update ticket type
        Task<bool> UpdateTicketTypeAsync(
            Guid ticketTypeId,
            UpdateTicketTypeRequest request,
            Guid organizerId);

        // Delete ticket type
        Task<bool> DeleteTicketTypeAsync(
            Guid ticketTypeId,
            Guid organizerId);
    }
}