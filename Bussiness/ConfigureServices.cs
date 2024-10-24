using Data;
using Bussiness.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Bussiness;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureBussinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDataLayer(configuration);
        services.AddScoped<ProductosServices>();
        services.AddScoped<ProductosVendidosServices>();
        services.AddScoped<UsuariosServices>();
        services.AddScoped<VentasServices>();
        return services;    
    }
}

