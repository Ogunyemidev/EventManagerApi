using Microsoft.EntityFrameworkCore;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.IRepositories;
using MyApi.Domain.Entities;
using MyApi.Infrastructure.Persistence;
using MyApi.Domain.Enums;


namespace MyApi.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetUpcomingEventsAsync(
            int pageNumber,
            int pageSize)
        {
            return await _context.Events
                .Where(x =>
                    x.EventDate > DateTime.UtcNow &&
                    x.EventStatus != EventStatus.Cancelled)
                .OrderBy(x => x.EventDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Event>> SearchEventsAsync(
            SearchEventRequest request)
        {
            var query = _context.Events
                .AsNoTracking()
                .Where(x =>
                    x.EventStatus != EventStatus.Cancelled);

            // Example search by event name
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x =>
                    x.EventName.Contains(
                        request.SearchTerm));
            }

            // Example venue search
            if (!string.IsNullOrWhiteSpace(request.Venue))
            {
                query = query.Where(x =>
                    x.Eventvenue.Contains(
                        request.Venue));
            }

            if (request.EventDate.HasValue)
            {
                var date = request.EventDate.Value.Date;

                query = query.Where(x =>
                    x.EventDate.Date == date);
            }

            return await query
                .OrderBy(x => x.EventDate)
                .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(
            Guid eventId)
        {
            return await _context.Events
                .FirstOrDefaultAsync(x =>
                    x.Id == eventId);
        }

        public async Task AddAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}