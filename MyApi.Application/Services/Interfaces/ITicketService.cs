using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;

namespace MyApi.Application.Services.Interfaces
{
    public interface ITicketServices
    {
        // US-BE-006
        Task<TicketDto> GenerateTicketAsync(
            Guid bookingId,
            Guid ticketTypeId,
            string holderName);

        // Generate tickets for an entire confirmed booking
        Task<List<TicketDto>> GenerateTicketsForBookingAsync(
            Guid bookingId);

        // Retrieve a specific ticket
        Task<TicketDto> GetTicketByIdAsync(
            Guid ticketId);

        // Customer's tickets
        Task<List<TicketDto>> GetMyTicketsAsync(
            Guid userId);

        // US-BE-009
        Task<TicketValidationResponse> ValidateTicketAsync(
            ValidateTicketRequest request,
            Guid staffUserId);

            

        // Cancel ticket
        Task<bool> CancelTicketAsync(
            Guid ticketId);
    }
}