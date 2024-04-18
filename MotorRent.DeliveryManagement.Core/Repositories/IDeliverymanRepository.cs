using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.DeliveryManagement.Core.Repositories;

public interface IDeliverymanRepository
{
    Deliveryman GetById(int id);
    Task<IEnumerable<Deliveryman>> GetAllAsync();
    Deliveryman GetByCNPJ(string cnpj);
    Deliveryman GetByDriverLicenseNumber(string driverLicenseNumber);
    void Add(Deliveryman deliveryman);
    void Update(Deliveryman deliveryman);
    void Remove(Deliveryman deliveryman);
}
