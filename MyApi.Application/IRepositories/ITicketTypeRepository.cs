using MyApi.Domain.Entities;

namespace MyApi.Application.IRepositories
{
    public interface ITicketTypeRepository
    {
        Task<TicketType> CreateAsync(TicketType ticketType);

        Task<List<TicketType>> GetByEventIdAsync(Guid eventId);

        Task<TicketType?> GetByIdAsync(Guid ticketTypeId);

        Task UpdateAsync(TicketType ticketType);

        Task DeleteAsync(TicketType ticketType);

        Task<bool> ExistsAsync(Guid ticketTypeId);
    }
}