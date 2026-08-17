using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyApi.Domain.Entities;

namespace MyApi.Application.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User LoggedInUser()
        {
            var email = LoggedInUserEmail();
            if (string.IsNullOrWhiteSpace(email))
            {
                return new User
                {
                    Email = string.Empty,
                    FirstName = string.Empty,
                    LastName = string.Empty
                };
            }

            return new User
            {
                Email = email,
                FirstName = string.Empty,
                LastName = string.Empty
            };
        }

        public string LoggedInUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }

        public Guid LoggedInUserId()
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(id, out var userId) ? userId : Guid.Empty;
        }
    }
}