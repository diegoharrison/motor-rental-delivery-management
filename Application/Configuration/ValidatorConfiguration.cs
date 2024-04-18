using Microsoft.Extensions.DependencyInjection;
using MotorRent.DeliveryManagement.Application.Validators;

namespace MotorRent.DeliveryManagement.Application.Configuration;

public static class ValidatorConfiguration
{
    public static void ConfigureValidators(IServiceCollection services)
    {
        services.AddScoped<DeliverymanValidator>();
        services.AddScoped<MotoValidator>();
    }
}
