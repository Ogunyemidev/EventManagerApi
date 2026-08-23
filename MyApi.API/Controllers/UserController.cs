
using MyApi.Application.Dtos.RequestDtos;
using MyApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Application.Dtos.ResponseDtos;
using MyApi.Application.Services.Implementations;
using MyApi.Domain.Entities;

namespace MyApi.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] NewUserRequest request)
        {
            var response = await _userService.CreateUserAsync(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request)
        {
            var response = await _userService.LoginAsync(request);
            return Ok(response);
        }

        // [Authorize(Policy = "AdminOnly")]
        // [HttpPost("add-user")]
        // public async Task<IActionResult> AddUser([FromBody] NewUserRequest request)
        // {
        //     var response = await _userService.AddUser(request);
        //     return Ok(response);
        // }

        // [HttpGet("profile")]
        // public async Task<IActionResult> GetProfile([FromQuery] Guid id)
        // {
        //     var response = await _userService.GetProfile(id);
        //     return Ok(response);
        // }

        // [HttpPost("upload-profile-picture")]
        // public async Task<IActionResult> UploadProfilePicture(
        //     IFormFile file,
        //     CancellationToken cancellationToken)
        // {
        //     var path = await _userService.UploadProfilePicture(
        //         file,
        //         cancellationToken);

        //     return Ok(path);
        // }
    }
}