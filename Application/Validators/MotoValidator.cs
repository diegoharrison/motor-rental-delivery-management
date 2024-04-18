using FluentValidation;
using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.DeliveryManagement.Application.Validators;

public class MotoValidator : AbstractValidator<Moto>
{
    public MotoValidator()
    {
        RuleFor(m => m.Identifier).NotEmpty();
        RuleFor(m => m.Year).GreaterThan(0);
        RuleFor(m => m.Model).NotEmpty();
        RuleFor(m => m.Plate).NotEmpty();
    }
}
