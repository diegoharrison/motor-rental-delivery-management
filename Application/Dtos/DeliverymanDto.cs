namespace MotorRent.DeliveryManagement.Application.Dtos;

public class DeliverymanDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string DriverLicenseNumber { get; set; }
    public string DriverLicenseType { get; set; }
    public byte[] DriverLicenseImage { get; set; }
}