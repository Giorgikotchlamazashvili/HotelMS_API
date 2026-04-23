namespace HotelMS.Application.Interfaces.Services
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(byte[] fileBytes, string fileName, string contentType, string folder = "default");
    }
}
