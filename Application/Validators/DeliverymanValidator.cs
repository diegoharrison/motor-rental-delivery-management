using FluentValidation;
using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.DeliveryManagement.Application.Validators;

public class DeliverymanValidator : AbstractValidator<Deliveryman>
{
    public DeliverymanValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
        RuleFor(d => d.CNPJ).NotEmpty();
        RuleFor(d => d.DateOfBirth).NotEmpty();
        RuleFor(d => d.DriverLicenseNumber).NotEmpty();
        RuleFor(d => d.DriverLicenseType).NotEmpty();
        RuleFor(d => d.DriverLicenseImage).NotEmpty();
    }
}
