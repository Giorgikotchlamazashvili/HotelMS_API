using HotelMS.Application.DTOs.Response.UserResponse;
using HotelMS.Application.DTOs.UserRequest;
using HotelMS.Application.Request.User;
namespace HotelMS.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<(bool success, string error, UserDto data)> CreateUserAsync(CreateUser dto);
        Task<(bool success, string error, UserDto data)> VerifyUserAsync(VerifyUser request);
        Task<(bool success, string error, AuthResponse data)> LoginUserAsync(LoginRequest req);
        Task<(bool success, string error, AuthResponse data)> RefreshTokenAsync(RefreshTokenRequest req);
        Task<(bool success, string error, List<UserDto> data)> GetAllUsersAsync();
        Task<(bool success, string error)> ForgetPasswordAsync(string email);
        Task<(bool success, string error)> ResetPasswordAsync(ResetPasswordRequest req);
        Task<(bool success, string error)> AddNewEmployeeAsync(AddNewEmployeeRequest req);

    }
}