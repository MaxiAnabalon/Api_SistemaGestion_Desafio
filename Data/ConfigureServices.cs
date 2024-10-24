using Microsoft.Extensions.DependencyInjection;
using Data.Context;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Data;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureDataLayer(this IServiceCollection services,
        IConfiguration configuration
        ) 
    {
        services.AddDbContext<DBConnection>(
           optionBuilder => { 
               var connectionString = configuration.GetConnectionString("DataBase");
               optionBuilder.UseSqlServer(connectionString);
           }
            );
        services.AddScoped<ProductosDB>();
        services.AddScoped<ProductoVendidoDB>();
        services.AddScoped<UsuarioDB>();
        services.AddScoped<VentaDB>();
        return services;    
    }
}
