using AutoMapper;
using HotelMS.Application.DTOs.Response.UserResponse;
using HotelMS.Application.DTOs.UserRequest;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Application.Request.User;
using HotelMS.Domain.Entities;
using HotelMS.Domain.Enums;
using HotelMS.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace HotelMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPasswordHashing _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _config;
        private readonly ICodeGenerator _generator;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IEmailService emailService,
            IPasswordHashing passwordHasher,
            ITokenGenerator tokenGenerator,
            IConfiguration config,
            ICodeGenerator generator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _config = config;
            _generator = generator;
        }

        public async Task<(bool success, string error, UserDto data)> CreateUserAsync(CreateUser dto)
        {
            if (await _userRepository.UserExistsByEmailAsync(dto.Email))
            {
                return (false, "User Already Exists", null);
            }
            var user = _mapper.Map<Users>(dto);
            user.Password = _passwordHasher.HashPassword(dto.Password);
            user.ValidationCode = _generator.GenerateCode();
            user.IsValidated = false;
            user.UserRole = UserRoles.StandardUser;
            user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            await _emailService.SendEmailAsync(
                user.Email,
                "Verify your email",
                $"Hello {user.Name}, your verification code is: {user.ValidationCode}");

            return (true, null, _mapper.Map<UserDto>(user));
        }

        public async Task<(bool success, string error, UserDto data)> VerifyUserAsync(VerifyUser request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return (false, "User not found.", null);
            }

            if (user.IsValidated)
            {
                return (false, "User is already validated.", null);
            }

            if (user.ValidationCode != request.Code)
            {
                return (false, "Invalid verification code.", null);
            }

            user.IsValidated = true;
            user.ValidationCode = null;

            await _userRepository.SaveChangesAsync();

            return (true, null, _mapper.Map<UserDto>(user));
        }

        public async Task<(bool success, string error, AuthResponse data)> LoginUserAsync(LoginRequest req)
        {
            var user = await _userRepository.GetUserByEmailAsync(req.Email);

            if (user == null)
            {
                return (false, "Invalid email or password.", null);
            }

            if (!_passwordHasher.VerifyPassword(req.Password, user.Password))
            {
                return (false, "Invalid email or password.", null);
            }

            if (!user.IsValidated)
            {
                return (false, "Please verify your email first.", null);
            }

            var accessToken = _tokenGenerator.GenerateAccessToken(user);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepository.SaveChangesAsync();

            int accessTokenMinutes = int.Parse(_config["Jwt:AccessTokenMinutes"]!);

            return (true, null, new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = accessTokenMinutes * 60,
                UserId = user.Id,
                Role = user.UserRole.ToString()
            });
        }

        public async Task<(bool success, string error, AuthResponse data)> RefreshTokenAsync(RefreshTokenRequest req)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(req.RefreshToken);

            if (user == null)
            {
                return (false, "Invalid or expired refresh token.", null);
            }

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return (false, "Invalid or expired refresh token.", null);
            }

            var newAccessToken = _tokenGenerator.GenerateAccessToken(user);
            var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepository.SaveChangesAsync();

            int accessTokenMinutes = int.Parse(_config["Jwt:AccessTokenMinutes"]!);

            return (true, null, new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresIn = accessTokenMinutes * 60,
                UserId = user.Id,
                Role = user.UserRole.ToString()
            });
        }

        public async Task<(bool success, string error, List<UserDto> data)> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return (false, "No users found.", null);
            }

            return (true, null, _mapper.Map<List<UserDto>>(users));
        }

        public async Task<(bool success, string error)> ForgetPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return (false, "No users found.");
            }

            var recoveryCode = _generator.GenerateCode();
            user.ValidationCode = recoveryCode;

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            await _emailService.SendEmailAsync(
                email,
                "Verify your email",
                $"Hello, your Recovery code is: {recoveryCode}");

            return (true, null);
        }

        public async Task<(bool success, string error)> ResetPasswordAsync(ResetPasswordRequest req)
        {
            var user = await _userRepository.GetUserByEmailAsync(req.Email);

            if (user == null)
            {
                return (false, "No users found.");
            }

            if (user.ValidationCode != req.Code)
            {
                return (false, "Wrong One Time Code.");
            }

            if (req.NewPassword != req.ConfirmPassword)
            {
                return (false, "Passwords do not match.");
            }

            user.Password = _passwordHasher.HashPassword(req.NewPassword);
            user.ValidationCode = null;

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool success, string error)> AddNewEmployeeAsync(AddNewEmployeeRequest req)
        {
            if (await _userRepository.UserExistsByEmailAsync(req.Email))
            {
                return (false, "User Already Exists.");
            }

            if (!Enum.IsDefined(typeof(UserRoles), req.Role))
            {
                return (false, "არასწორი როლი მითითებული.");
            }

            var user = _mapper.Map<Users>(req);
            user.Password = _passwordHasher.HashPassword(req.Password);
            user.IsValidated = true;
            user.UserRole = req.Role;
            user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddNewEmployeeAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, null);
        }
    }
}