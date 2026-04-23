using HotelMS.Domain.Common;
using HotelMS.Domain.Enums;

namespace HotelMS.Domain.Entities
{
    public class Users : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles UserRole { get; set; }
        public UserDetails UserDetails { get; set; }
        public bool IsValidated { get; set; } = false;
        public string? ValidationCode { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
