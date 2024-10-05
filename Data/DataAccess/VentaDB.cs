using Entidades;
using Data.Context;

namespace Data.DataAccess;
public class VentaDB
{
    private readonly DBConnection _conexion;

    // Constructor que utiliza la inyección de dependencias
    public VentaDB(DBConnection conexion)
    {
        _conexion = conexion;
    }

    public Venta? OneVenta(int Id)
    {
        return _conexion.ventas.FirstOrDefault(ven => ven.Id == Id);

    }
    public List<Venta> GetVentas()
    {
        return _conexion.ventas.ToList();
    }

    public bool UpdateVenta(int Id, Venta venta)
    {
        try
        {
            var ventaEditar = OneVenta(Id);
            if (ventaEditar != null)
            {
                ventaEditar.Id = venta.Id;
                ventaEditar.comentario = venta.comentario;
                ventaEditar.IdUsuario = venta.IdUsuario;
                _conexion.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            // Aquí podrías registrar el error
            Console.WriteLine($"Error al actualizar la venta: {ex.Message}");
            return false; // Opcional: lanzar de nuevo la excepción si necesitas manejarla más arriba
        }
    }

    public Venta InsertVenta(Venta venta)
    {
        _conexion.ventas.Add(venta);
        _conexion.SaveChanges();
        return venta;
    }
    public bool DeleteVenta(int Id)
    {
        try
        {
            var ventaAEliminar = OneVenta(Id);
            if (ventaAEliminar != null)
            {
                _conexion.ventas.Remove(ventaAEliminar);
                _conexion.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            // Aquí podrías registrar el error
            Console.WriteLine($"Error al eliminar venta: {ex.Message}");
            return false; // Opcional: lanzar de nuevo la excepción si necesitas manejarla más arriba
        }
    }
}

