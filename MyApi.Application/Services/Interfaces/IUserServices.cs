using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Domain.Entities;


namespace MyApi.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> UpdateWalletBalanceAsync(Guid id, decimal amount);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> SearchUserRequest();
    }
}