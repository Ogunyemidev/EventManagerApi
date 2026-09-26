using Microsoft.EntityFrameworkCore;
using MyApi.Application.IRepositories;
using MyApi.Domain.Entities;
using MyApi.Infrastructure.Persistence;


namespace MyApi.Infrastructure.Persistence.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly AppDbContext _context;

        public TicketTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TicketType> CreateAsync(TicketType ticketType)
        {
            await _context.TicketTypes.AddAsync(ticketType);
            await _context.SaveChangesAsync();

            return ticketType;
        }
        public async Task<List<TicketType>> GetAllAsync()
        {
            return await _context.TicketTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TicketType>> GetAllTicketTypesAsync()
        {
            return await GetAllAsync();
        }

        public async Task<List<TicketType>> GetByEventIdAsync(Guid eventId)
        {
            return await _context.TicketTypes
                .Where(x => x.EventId == eventId)
                .ToListAsync();
        }

        public async Task<TicketType?> GetByIdAsync(Guid ticketTypeId)
        {
            return await _context.TicketTypes
                .FirstOrDefaultAsync(x => x.TicketTypeId == ticketTypeId);
        }

        public async Task UpdateAsync(TicketType ticketType)
        {
            _context.TicketTypes.Update(ticketType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TicketType ticketType)
        {
            _context.TicketTypes.Remove(ticketType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid ticketTypeId)
        {
            return await _context.TicketTypes
                .AnyAsync(x => x.TicketTypeId == ticketTypeId);
        }
    }
}