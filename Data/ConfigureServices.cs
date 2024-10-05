using Microsoft.Extensions.DependencyInjection;
using Data.Context;
using Data.DataAccess;

namespace Data;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureDataLayer(this IServiceCollection services) 
    {
        services.AddDbContext<DBConnection>();
        services.AddScoped<ProductosDB>();
        services.AddScoped<ProductoVendidoDB>();
        services.AddScoped<UsuarioDB>();
        services.AddScoped<VentaDB>();
        return services;    
    }
}
