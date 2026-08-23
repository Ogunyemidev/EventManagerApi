using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<LoginResponse> CreateUserAsync(NewUserRequest request);

        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task<List<UserDto>> GetAllUsers(SearchUserRequest request);

        Task<UserDto> GetProfile(Guid id);

        Task<UserDto> GetUserByEmail(string email);

        Task<bool> UpdateProfile(
            Guid id,
            UpdateUserRequest request);

        Task<string> UploadProfilePicture(
            IFormFile file,
            CancellationToken cancellationToken);
    }
}