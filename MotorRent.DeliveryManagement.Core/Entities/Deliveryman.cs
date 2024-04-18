namespace MotorRent.DeliveryManagement.Core.Entities;

public class Deliveryman
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string DriverLicenseNumber { get; set; }
    public string DriverLicenseType { get; set; }
    public string DriverLicenseImage { get; set; }
}
