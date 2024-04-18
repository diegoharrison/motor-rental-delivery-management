using Microsoft.Extensions.DependencyInjection;
using MotorRent.DeliveryManagement.Application.S3;
using MotorRent.DeliveryManagement.Application.S3.Interfaces;
using MotorRent.DeliveryManagement.Application.Services;
using MotorRent.DeliveryManagement.Infrastructure;

namespace MotorRent.DeliveryManagement.Application.Configuration;

public static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<DeliverymanService>();
        services.AddScoped<MotoService>();
        services.AddScoped<IS3Service, S3Service>();

        RepositoryConfiguration.ConfigureRepositories(services);
        ValidatorConfiguration.ConfigureValidators(services);
    }
}