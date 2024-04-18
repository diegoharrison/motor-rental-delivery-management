using MotorRent.DeliveryManagement.Application.Common;
using MotorRent.DeliveryManagement.Application.Services.Interfaces;
using MotorRent.DeliveryManagement.Application.Validators;
using MotorRent.DeliveryManagement.Core.Entities;
using MotorRent.DeliveryManagement.Core.Repositories;

namespace MotorRent.DeliveryManagement.Application.Services;

public class MotoService : IMotoService
{
    private readonly IMotoRepository _motoRepository;
    private readonly MotoValidator _validator;

    public MotoService(IMotoRepository motoRepository, MotoValidator validator)
    {
        _motoRepository = motoRepository;
        _validator = validator;
    }

    public async Task<Moto> GetMotoByIdAsync(int id)
    {
        return _motoRepository.GetById(id);
    }

    public async Task<IEnumerable<Moto>> GetAllMotosAsync()
    {
        return await _motoRepository.GetAllAsync();
    }

    public async Task<OperationResult> AddMoto(Moto moto)
    {
        var validationResult = await _validator.ValidateAsync(moto);
        
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new OperationResult { Success = false, Message = $"Validation failed: {errors}" };
        }

        var existingMoto = _motoRepository.GetByPlate(moto.Plate);
        if (existingMoto != null)
        {
            return new OperationResult { Success = false, Message = "A motorcycle with this plate already exists." };
        }            
        
        var motorCycle = new Moto
        {
            Identifier = moto.Identifier,
            Year = moto.Year,
            Model = moto.Model,
            Plate = moto.Plate
        };

        _motoRepository.Add(motorCycle);

        return new OperationResult { Success = true, Message = "Motorcycle added successfully." };
    }

    public async Task<OperationResult> UpdateMotoAsync(Moto moto)
    {
        var existingMoto = _motoRepository.GetById(moto.Id);
        
        if (existingMoto == null)
        {
            return new OperationResult { Success = false, Message = "A motorcycle with this plate already exists." };
        }

        existingMoto.Identifier = moto.Identifier;
        existingMoto.Year = moto.Year;
        existingMoto.Model = moto.Model;
        existingMoto.Plate = moto.Plate;

        _motoRepository.Update(existingMoto);

        return new OperationResult { Success = true, Message = "Motorcycle updated successfully." };
    }

    public async Task<OperationResult> RemoveMotoAsync(int id)
    {
        var existingMoto = _motoRepository.GetById(id);
        
        if (existingMoto == null)
        {
            return new OperationResult { Success = false, Message = "Motorcycle not found." };
        }

        _motoRepository.Remove(existingMoto);

        return new OperationResult { Success = true, Message = "Motorcycle removed successfully." };
    }
}