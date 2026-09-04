using Microsoft.EntityFrameworkCore;
using MyApi.Application.IRepositories;
using MyApi.Domain.Entities;
using MyApi.Infrastructure.Persistence;

namespace MyApi.Infrastructure.Persistence.Repositories
{
public class TicketRepository : ITicketRepository
{
private readonly AppDbContext _context;


    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetTicketByIdAsync(Guid ticketId)
    {
        return await _context.Tickets
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    public async Task<List<Ticket>> GetTicketsByBookingIdAsync(Guid bookingId)
    {
        return await _context.Tickets
            .Include(t => t.TicketType)
            .Where(t => t.BookingId == bookingId)
            .ToListAsync();
    }

    public async Task<List<Ticket>> GetTicketsByEventIdAsync(Guid eventId)
    {
        return await _context.Tickets
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .Where(t => t.Booking.EventId == eventId)
            .ToListAsync();
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}


}
