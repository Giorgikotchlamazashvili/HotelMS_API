using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using HotelMS.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace HotelMS.Infrastructure.ExternalServices
{
    public class S3Services : IStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly string _regionName;

        public S3Services(IConfiguration config)
        {
            var accessKey = config["AWS:AccessKey"];
            var secretKey = config["AWS:SecretKey"];
            _bucketName = config["AWS:BucketName"];
            _regionName = config["AWS:Region"];

            _s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(_regionName));
        }


        public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName, string contentType, string folder = "default")
        {
            var fullPath = $"{folder}/{Guid.NewGuid()}_{fileName}";

            using var stream = new MemoryStream(fileBytes);

            var req = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fullPath,
                InputStream = stream,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            };

            await _s3Client.PutObjectAsync(req);
            return $"https://{_bucketName}.s3.{_regionName}.amazonaws.com/{fullPath}";
        }
    }
}