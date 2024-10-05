using Data.DataAccess;
using Entidades;

namespace Bussiness.Services;

public class ProductosServices
{
    private ProductosDB _productosDataAccess;

    public ProductosServices (ProductosDB ProductosDataAccess) 
    {   
        _productosDataAccess = ProductosDataAccess;
    }

    public Producto? OneProductoSer(int Id)
    {
        return _productosDataAccess.OneProducto(Id);

    }
    public List<Producto> GetProductosSer()
    {
        return _productosDataAccess.GetProductos() ;
    }

    public bool UpdateProductoSer(int Id, Producto producto)
    {
        return _productosDataAccess.UpdateProducto(Id, producto);   
    }

    public Producto InsertProductoSer(Producto producto)
    {
        return _productosDataAccess.InsertProducto(producto);
    }
    public bool DeleteProductoSer(int Id)
    {
        return _productosDataAccess.DeleteProducto(Id);
    }


}

