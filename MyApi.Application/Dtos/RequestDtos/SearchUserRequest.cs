using MyApi.Domain;
using MyApi.Domain.Enums;

namespace MyApi.Application.Dtos.RequestDtos
{
    public class SearchUserRequest : PaginationRequest
    {
        public Role? Role { get; set; }
        public SortBy? SortBy { get; set; }
    }

    public enum SortBy
    {
        FirstName,
        LastName,
        Email,
        Role
    }
}