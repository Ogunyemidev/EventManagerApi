using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Domain.Entities;

namespace MyApi.Application
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
        Task<List<User>> GetAllUsers();
        Task<List<User>> SearchUsers(SearchUserRequest request);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateWalletBalance(Guid id, decimal amount);
        Task<bool> EmailExists(string email);
    }
}