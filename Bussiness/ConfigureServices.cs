using Data;
using Bussiness.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bussiness;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureBussinessLayer(this IServiceCollection services)
    {
        services.ConfigureDataLayer();
        services.AddScoped<ProductosServices>();
        services.AddScoped<ProductosVendidosServices>();
        services.AddScoped<UsuariosServices>();
        services.AddScoped<VentasServices>();
        return services;    
    }
}

