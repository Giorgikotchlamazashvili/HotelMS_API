using HotelMS.Application.DTOs.UserRequest;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Application.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUser dto)
        {
            var (success, error, data) = await _userService.CreateUserAsync(dto);

            if (!success)
            {
                return BadRequest(error);
            }

            return Ok(data);
        }

        [HttpPost("verify-user")]
        public async Task<IActionResult> VerifyUser(VerifyUser request)
        {
            var (success, error, data) = await _userService.VerifyUserAsync(request);

            if (!success)
            {
                return BadRequest(error);
            }

            return Ok(data);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser(LoginRequest req)
        {
            var (success, error, data) = await _userService.LoginUserAsync(req);

            if (!success)
            {
                return Unauthorized(error);
            }

            return Ok(data);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest req)
        {
            var (success, error, data) = await _userService.RefreshTokenAsync(req);

            if (!success)
            {
                return Unauthorized(error);
            }

            return Ok(data);
        }

        [Authorize(Roles = "Manager , Receptionist , Admin")]
        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUsers()
        {
            var (success, error, data) = await _userService.GetAllUsersAsync();

            if (!success)
            {
                return NotFound(error);
            }

            return Ok(data);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var (success, error) = await _userService.ForgetPasswordAsync(email);
            if (!success)
            {
                return BadRequest(error);
            }

            return Ok("Password reset link sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest req)
        {
            var (success, error) = await _userService.ResetPasswordAsync(req);
            if (!success)
            {
                return BadRequest(error);
            }
            return Ok("Password has been reset successfully.");
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPost("add-new-employee")]
        public async Task<IActionResult> AddNewEmployee(AddNewEmployeeRequest req)
        {
            var (success, error) = await _userService.AddNewEmployeeAsync(req);
            if (!success)
            {
                return BadRequest(error);
            }
            return Ok("New employee added successfully.");
        }
    }
}