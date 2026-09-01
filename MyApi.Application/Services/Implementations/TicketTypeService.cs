using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Application.IRepositories;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;


namespace MyApi.Application.Services.Implementations
{
    public class TicketTypeServices : ITicketTypeServices
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;

        public TicketTypeServices(
            ITicketTypeRepository ticketTypeRepository)
        {
            _ticketTypeRepository = ticketTypeRepository;
        }

        // CREATE
        public async Task<TicketTypeDto> CreateTicketTypeAsync(
            CreateTicketTypeRequest request,
            Guid organizerId)
        {
            if (request.EventId == Guid.Empty)
                throw new ArgumentException("Event ID is required.");

            if (string.IsNullOrWhiteSpace(request.TicketTypeName))
                throw new ArgumentException("Ticket type name is required.");

            if (request.Price < 0)
                throw new ArgumentException("Price cannot be negative.");

            if (request.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            // TODO:
            // Verify that organizerId owns the event.
            // This requires your EventRepository.

            var ticketType = new TicketType
            {
                TicketTypeId = Guid.NewGuid(),

                EventId = request.EventId,

                Name = request.TicketTypeName,

                Description = request.Description,

                Price = request.Price,

                Capacity = request.Quantity,

                RemainingQuantity = request.Quantity
            };

            var createdTicketType =
                await _ticketTypeRepository.CreateAsync(ticketType);

            return MapToDto(createdTicketType);
        }


        // GET ALL TICKET TYPES FOR EVENT
        public async Task<List<TicketTypeDto>> GetTicketTypesByEventIdAsync(
            Guid eventId)
        {
            if (eventId == Guid.Empty)
                throw new ArgumentException("Event ID is required.");

            var ticketTypes =
                await _ticketTypeRepository.GetByEventIdAsync(eventId);

            return ticketTypes
                .Select(MapToDto)
                .ToList();
        }


        // GET SINGLE TICKET TYPE
        public async Task<TicketTypeDto> GetTicketTypeByIdAsync(
            Guid ticketTypeId)
        {
            if (ticketTypeId == Guid.Empty)
                throw new ArgumentException("Ticket type ID is required.");

            var ticketType =
                await _ticketTypeRepository.GetByIdAsync(ticketTypeId);

            if (ticketType == null)
                throw new KeyNotFoundException(
                    "Ticket type not found.");

            return MapToDto(ticketType);
        }


        // UPDATE
        public async Task<bool> UpdateTicketTypeAsync(
            Guid ticketTypeId,
            UpdateTicketTypeRequest request,
            Guid organizerId)
        {
            if (ticketTypeId == Guid.Empty)
                throw new ArgumentException("Ticket type ID is required.");

            var ticketType =
                await _ticketTypeRepository.GetByIdAsync(ticketTypeId);

            if (ticketType == null)
                return false;

            if (string.IsNullOrWhiteSpace(request.TicketTypeName))
                throw new ArgumentException(
                    "Ticket type name is required.");

            if (request.Price < 0)
                throw new ArgumentException(
                    "Price cannot be negative.");

            if (request.Quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.");

            // TODO:
            // Verify that organizerId owns ticketType.EventId.

            /*
             * Preserve tickets that have already been sold.
             *
             * Example:
             * Capacity = 100
             * RemainingQuantity = 80
             *
             * 20 tickets have been sold.
             *
             * If organizer changes capacity to 120,
             * RemainingQuantity becomes 100.
             */

            var soldQuantity =
                ticketType.Capacity - ticketType.RemainingQuantity;

            if (request.Quantity < soldQuantity)
            {
                throw new ArgumentException(
                    $"Quantity cannot be less than the number of tickets already sold ({soldQuantity}).");
            }

            ticketType.Name = request.TicketTypeName;

            ticketType.Description = request.Description;

            ticketType.Price = request.Price;

            ticketType.Capacity = request.Quantity;

            ticketType.RemainingQuantity =
                request.Quantity - soldQuantity;

            await _ticketTypeRepository.UpdateAsync(ticketType);

            return true;
        }


        // DELETE
        public async Task<bool> DeleteTicketTypeAsync(
            Guid ticketTypeId,
            Guid organizerId)
        {
            if (ticketTypeId == Guid.Empty)
                throw new ArgumentException("Ticket type ID is required.");

            var ticketType =
                await _ticketTypeRepository.GetByIdAsync(ticketTypeId);

            if (ticketType == null)
                return false;

            // TODO:
            // Verify organizer owns ticketType.EventId.

            var soldQuantity =
                ticketType.Capacity - ticketType.RemainingQuantity;

            if (soldQuantity > 0)
            {
                throw new InvalidOperationException(
                    "Cannot delete a ticket type that has already sold tickets.");
            }

            await _ticketTypeRepository.DeleteAsync(ticketType);

            return true;
        }


        // MAPPING
        private static TicketTypeDto MapToDto(
            TicketType ticketType)
        {
            return new TicketTypeDto
            {
                TicketTypeId = ticketType.TicketTypeId,

                EventId = ticketType.EventId,

                TicketTypeName = ticketType.Name,

                Description = ticketType.Description,

                Price = ticketType.Price,

                Quantity = ticketType.Capacity,

                AvailableQuantity =
                    ticketType.RemainingQuantity,

                CreatedDate = ticketType.CreatedDate
            };
        }
    }
}