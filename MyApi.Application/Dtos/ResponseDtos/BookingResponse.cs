// using MyApi.Domain.Entities;
// using MyApi.Domain.Enums;


// namespace MyApi.Application.Dtos.ResponseDtos
// {
//     public class BookingDto
//     {
//         public Guid BookingId { get; set; }

//         public Guid CustomerId { get; set; }

//         public Guid EventId { get; set; }

//         public string EventName { get; set; } = string.Empty;

//         public BookingStatus BookingStatus { get; set; }

//         public DateTime ReservedAt { get; set; }

//         public DateTime ExpiresAt { get; set; }

//         public decimal TotalAmount { get; set; }

//         public List<BookingItemDto> Items { get; set; }
//             = new();
//     }

//     public class BookingItemDto
//     {
//         public Guid TicketTypeId { get; set; }

//         public string TicketTypeName { get; set; } = string.Empty;

//         public int Quantity { get; set; }

//         public decimal UnitPrice { get; set; }

//         public decimal TotalPrice { get; set; }
//     }
// }