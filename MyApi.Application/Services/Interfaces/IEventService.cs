using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;

namespace MyApi.Application.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetUpcomingEventsAsync(
            int pageNumber = 1,
            int pageSize = 10);

        Task<List<EventDto>> SearchEventsAsync(
            SearchEventRequest request);

        Task<EventDetailsDto> GetEventByIdAsync(
            Guid eventId);

        Task<EventDto> CreateEventAsync(
            CreateEventRequest request,
            Guid organizerId);

        Task<bool> UpdateEventAsync(
            Guid eventId,
            UpdateEventRequest request,
            Guid organizerId);

        Task<bool> CancelEventAsync(
            Guid eventId,
            Guid organizerId);
    }
}