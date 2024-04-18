using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.DeliveryManagement.Core.Repositories;

public interface IMotoRepository
{
    Moto GetById(int id);
    Task<IEnumerable<Moto>> GetAllAsync();
    Moto GetByPlate(string plate);
    void Add(Moto moto);
    void Update(Moto moto);
    void Remove(Moto moto);
}
