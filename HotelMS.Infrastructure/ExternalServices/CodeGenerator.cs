using HotelMS.Application.Interfaces.Services;


namespace HotelMS.Infrastructure.ExternalServices
{
    public class CodeGenerator : ICodeGenerator
    {
        public string GenerateCode()
        {
            return new Random().Next(1000000000, int.MaxValue).ToString();
        }
    }
}