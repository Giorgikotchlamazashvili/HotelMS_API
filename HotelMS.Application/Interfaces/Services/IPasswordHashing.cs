namespace HotelMS.Application.Interfaces.Services
{
    public interface IPasswordHashing
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);

    }
}