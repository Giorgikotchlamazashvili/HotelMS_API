using HotelMS.Domain.Entities;

namespace HotelMS.Application.Interfaces.Services
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(Users user);
        string GenerateRefreshToken();
    }
}
