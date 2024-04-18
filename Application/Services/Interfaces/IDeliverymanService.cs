using MotorRent.DeliveryManagement.Application.Common;
using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.DeliveryManagement.Application.Services.Interfaces;

public interface IDeliverymanService
{
    Task<Deliveryman> GetDeliverymanByIdAsync(int id);
    Task<Deliveryman> GetDeliverymanByCnpjAsync(string cnpj);
    Task<Deliveryman> GetDeliverymanByDriverLicenseNumberAsync(string driverLicenseNumber);
    Task<IEnumerable<Deliveryman>> GetAllDeliverymenAsync();
    Task<OperationResult> CreateOrUpdateDeliveryManAsync(Deliveryman deliveryman);
}