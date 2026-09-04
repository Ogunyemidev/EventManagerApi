using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Domain.Entities;

namespace MyApi.Application.IRepositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetTicketsByBookingIdAsync(Guid bookingId);
        Task<Ticket?> GetTicketByIdAsync(Guid ticketId);

        Task AddTicketAsync(Ticket ticket);

        Task SaveChangesAsync();

        Task<List<Ticket>> GetTicketsByEventIdAsync(Guid eventId);
    }
}