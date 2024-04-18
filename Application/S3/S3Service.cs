using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using MotorRent.DeliveryManagement.Application.S3.Interfaces;

namespace MotorRent.DeliveryManagement.Application.S3
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _accessKeyId;
        private readonly string _secretAccessKey;
        private readonly RegionEndpoint _regionEndpoint;

        public S3Service(IConfiguration configuration)
        {
            //_accessKeyId = configuration["AwsS3Config:AccessKeyId"]!;
            //_secretAccessKey = configuration["AwsS3Config:SecretAccessKey"]!;
            //_regionEndpoint = RegionEndpoint.GetBySystemName(configuration["AwsS3Config:RegionEndpoint"]);

            //var credentials = new BasicAWSCredentials(_accessKeyId, _secretAccessKey);
            //_s3Client = new AmazonS3Client(credentials, _regionEndpoint);
        }

        public async Task<string> UploadImageToS3Async(string base64Image)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Image);

                string fileName = "nome-da-imagem.jpg";

                string bucketName = "seu-bucket-s3";

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = new MemoryStream(imageBytes),
                    ContentType = "image/jpeg" // Defina o tipo de conteúdo da imagem conforme necessário
                };
                
                var response = await _s3Client.PutObjectAsync(request);
                                
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    string imageUrl = $"https://{bucketName}.s3.amazonaws.com/{fileName}";
                    return imageUrl;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
