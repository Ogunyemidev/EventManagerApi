using MyApi.Application.Dtos.RequestDtos;
using MyApi.Domain.Entities;

namespace MyApi.Application.IRepositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetUpcomingEventsAsync(
            int pageNumber,
            int pageSize);

        Task<List<Event>> SearchEventsAsync(
            SearchEventRequest request);

        Task<Event?> GetByIdAsync(Guid eventId);

        Task AddAsync(Event eventEntity);

        Task SaveChangesAsync();
    }
}
