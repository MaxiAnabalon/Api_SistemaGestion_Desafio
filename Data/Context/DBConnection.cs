using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DBConnection : DbContext
{   
    public DbSet <Producto> Productos { get; set; }
    public DbSet <ProductoVendido> ProductosVendidos { get; set; }  
    public DbSet <Usuario> Usuarios { get; set; }   

    public DbSet <Venta> ventas {  get; set; }   

    public DBConnection(): base() { }

    public DBConnection(DbContextOptions<DBConnection> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=ANABALON\\SQLEXPRESS;Initial Catalog=CoderhousePreEntregaFinal;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    }

}


