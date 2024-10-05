using Entidades;
using Data.Context;

namespace Data.DataAccess;

public class ProductoVendidoDB
{
    private readonly DBConnection _conexion;

    public ProductoVendidoDB(DBConnection conexion)
    {
        _conexion = conexion;
    }

    public ProductoVendido? OneProductoVendido(int Id)
    {
        return _conexion.ProductosVendidos.FirstOrDefault(pro => pro.Id == Id);
    }
    public List<ProductoVendido> GetProductoVendido()
    {
        return _conexion.ProductosVendidos.ToList();
    }

    public bool UpdateProductoVendido(int Id, ProductoVendido productoVend)
    {
        try
        {
            var productoEditarVend = OneProductoVendido(Id);
            if (productoEditarVend != null)
            {
                productoEditarVend.IdProducto = productoVend.IdProducto; //nose si se tiene que cambiar?   
                productoEditarVend.stock = productoVend.stock;
                productoEditarVend.IdVenta = productoVend.IdVenta; //nose si se tiene que cambiar?
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
            Console.WriteLine($"Error al actualizar el producto vendido: {ex.Message}");
            return false; 
        }
    }

    public ProductoVendido InsertProductosVendido(ProductoVendido productoVend)
    {
        _conexion.ProductosVendidos.Add(productoVend);
        _conexion.SaveChanges();
        return productoVend;    
    }
    public bool DeleteProductoVendido(int Id)
    {
        try
        {
            var productoAEliminarVend = OneProductoVendido(Id);
            if (productoAEliminarVend != null)
            {
                _conexion.ProductosVendidos.Remove(productoAEliminarVend);
                _conexion.SaveChanges();
                return true;
            }
            else
            {
                return false ;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar el producto vendido: {ex.Message}");
            return false;
        }
    }
}

