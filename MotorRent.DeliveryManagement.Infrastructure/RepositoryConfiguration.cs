using Microsoft.Extensions.DependencyInjection;
using MotorRent.DeliveryManagement.Core.Repositories;
using MotorRent.DeliveryManagement.Infrastructure.Repositories;

namespace MotorRent.DeliveryManagement.Infrastructure;

public static class RepositoryConfiguration
{
    public static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<IDeliverymanRepository, DeliverymanRepository>();
        services.AddScoped<IMotoRepository, MotoRepository>();
    }
}