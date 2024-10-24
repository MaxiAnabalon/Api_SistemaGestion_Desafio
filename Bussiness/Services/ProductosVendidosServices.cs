using Data.DataAccess;
using Entidades;

namespace Bussiness.Services;

public class ProductosVendidosServices
{
    private ProductoVendidoDB _productosVendidosDataAccess;

    public ProductosVendidosServices(ProductoVendidoDB productoVendidoDataAccess)
    {
        _productosVendidosDataAccess = productoVendidoDataAccess;
    }

    public ProductosVendidosServices()
    {
    }

    public ProductoVendido? OneProductoVendidoSer(int Id)
    {
        return _productosVendidosDataAccess.OneProductoVendido(Id);
    }
    public List<ProductoVendido> GetProductosVendidosSer()
    {   
       return _productosVendidosDataAccess.GetProductoVendido();
    }

    public List<ProductoVendido> GetProductosVendidosPorVentaServices(int ventaID)
    {
        return _productosVendidosDataAccess.GetProductosVendidosPorVenta(ventaID);
    }

    public bool UpdateProductoVendidoSer(int Id, ProductoVendido productoVend)
    {
        return _productosVendidosDataAccess.UpdateProductoVendido(Id, productoVend);
    }

    public ProductoVendido InsertProductosVendidoSer(ProductoVendido productoVend)
    {
        return _productosVendidosDataAccess.InsertProductosVendido(productoVend);
    }
    public bool DeleteProductoVendidoSer(int Id)
    {
        return _productosVendidosDataAccess.DeleteProductoVendido(Id);
    }
}

