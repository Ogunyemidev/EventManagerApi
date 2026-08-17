using MyApi.Domain.Entities;
namespace MyApi.Application.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}