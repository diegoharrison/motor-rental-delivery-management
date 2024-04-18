using MotorRent.DeliveryManagement.Application.Common;
using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.DeliveryManagement.Application.Services.Interfaces;

public interface IMotoService
{
    Task<Moto> GetMotoByIdAsync(int id);
    Task<IEnumerable<Moto>> GetAllMotosAsync();
    Task<OperationResult> AddMoto(Moto moto);
    Task<OperationResult> UpdateMotoAsync(Moto moto);
    Task<OperationResult> RemoveMotoAsync(int id);
}