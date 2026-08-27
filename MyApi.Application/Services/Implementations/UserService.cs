using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyApi.Application.Authentication;
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Application.Services.Interfaces;
using MyApi.Application.IRepositories;
using MyApi.Application.Storage;
using MyApi.Application.Exceptions;


using MyApi.Domain;
using MyApi.Domain.Entities;
using MyApi.Domain.Enums;

namespace MyApi.Application.Services.Implementations
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IJwtService _jwtService;
        private readonly IFileStorage _fileStorage;
        private readonly ICurrentUser _currentUser;

        public UserService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IFileStorage fileStorage,
            ILogger<UserService> logger,
            ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _fileStorage = fileStorage;
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<List<UserDto>> GetAllUsers(SearchUserRequest request)
        {
            var users = await _userRepository.SearchUsers(request);

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToList();
        }

        public async Task<UserDto> GetProfile(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID: {id} not found.");
            }

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new NotFoundException($"User with email: {email} not found.");
            }

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            _logger.LogInformation("Attempting to log in user with email: {Email}", request.Email);

            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null || !Util.IsValidPassword(request.Password, user.HashedPassword))
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
                throw new UnauthorizedException("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                Id = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = user.Role.ToString()
            };
        }

    public async Task<LoginResponse> CreateUserAsync(NewUserRequest request)
{
    _logger.LogInformation(
        "Registering user with email: {Email}",
        request.Email);

    var alreadyExists = await _userRepository.EmailExists(request.Email);

    if (alreadyExists)
    {
        _logger.LogWarning(
            "User with email {Email} already exists.",
            request.Email);

        throw new BadRequestException(
            $"User with email: {request.Email} already exists.");
    }

    var newUser = new User
    {
        Id = Guid.NewGuid(),
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        HashedPassword = Util.EncryptPassword(request.Password),
        Role = request.Role,
        CreatedBy = request.Email
    };

    await _userRepository.AddUser(newUser);

    _logger.LogInformation(
        "User registered successfully with email: {Email}",
        request.Email);

    var token = _jwtService.GenerateToken(newUser);

    return new LoginResponse
    {
        Token = token,
        Id = newUser.Id,
        Email = newUser.Email,
        FullName = $"{newUser.FirstName} {newUser.LastName}",
        Role = newUser.Role.ToString()
    };
}

        public async Task<bool> UpdateProfile(Guid id, UpdateUserRequest request)
        {
            _logger.LogInformation("Updating profile for user with ID: {UserId}", id);

            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", id);
                throw new NotFoundException($"User with ID: {id} not found.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            return await _userRepository.UpdateUser(user);
        }

        public async Task<string> UploadProfilePicture(IFormFile file, CancellationToken cancellationToken)
        {
            if (file.Length == 0)
            {
                throw new BadRequestException("File is empty.");
            }

            var email = _currentUser.LoggedInUserEmail();
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                _logger.LogWarning("User not found.");
                throw new NotFoundException("User not found.");
            }

            await using var stream = file.OpenReadStream();
            var path = await _fileStorage.SaveAsync(new FileUploadRequest
            {
                Content = stream,
                FileName = $"{email}_{file.FileName}",
                Folder = "ProfilePictures",
                ContentType = file.ContentType
            }, cancellationToken);

            return path;
        }
    }
}