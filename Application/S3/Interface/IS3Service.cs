namespace MotorRent.DeliveryManagement.Application.S3.Interfaces;

public interface IS3Service
{
    Task<string> UploadImageToS3Async(string base64Image);
}