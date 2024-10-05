using Entidades;
using Data.Context;


namespace Data.DataAccess;

public class ProductosDB
{

    private readonly DBConnection _conexion;

    // Constructor que utiliza la inyección de dependencias
    public ProductosDB(DBConnection conexion)
    {
        _conexion = conexion;
    }
    public Producto? OneProducto(int Id)
    {
        return _conexion.Productos.FirstOrDefault(pro => pro.Id == Id);

    }
    public List<Producto> GetProductos()
    {
        return _conexion.Productos.ToList();
    }

    public bool UpdateProducto(int Id, Producto producto)
    {
        try
        {
            var productoEditar = OneProducto(Id);
            if (productoEditar != null)
            {
                productoEditar.descripcion = producto.descripcion;
                productoEditar.costo = producto.costo;
                productoEditar.precioVenta = producto.precioVenta;
                producto.stock = productoEditar.stock;
                producto.IdUsuario = producto.IdUsuario; //nose si se tiene que cambiar?
                _conexion.SaveChanges();
                return true;
            }
            else
            { return false; }
        }
        catch (Exception ex)
        {
            // Manejo de la excepción, puedes registrar el error
            Console.WriteLine($"Error al actualizar el producto: {ex.Message}");
            return false;
        }
    }

    public Producto InsertProducto(Producto producto)
    {
        _conexion.Productos.Add(producto);
        _conexion.SaveChanges();
        return producto;

    }
    public bool DeleteProducto(int Id)
    {
        try
        {
            var productoAEliminar = OneProducto(Id);
            if (productoAEliminar != null)
            {
                _conexion.Productos.Remove(productoAEliminar);
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
            // Manejo de la excepción, puedes registrar el error
            Console.WriteLine($"Error al eliminar el producto: {ex.Message}");
            return false;
        }

    }

}


