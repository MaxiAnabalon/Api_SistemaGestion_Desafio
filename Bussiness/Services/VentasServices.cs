using Data.DataAccess;
using Entidades;

namespace Bussiness.Services;
public class VentasServices 
{
    private VentaDB _ventasDataAccess;

    public VentasServices (VentaDB ventasDataAccess) 
    {
        _ventasDataAccess = ventasDataAccess;
    }
    public Venta? OneVentaSer(int Id)
    {
        return _ventasDataAccess.OneVenta(Id);

    }
    public List<Venta> GetVentasSer()
    {
        return _ventasDataAccess.GetVentas();
    }

    public bool UpdateVentaSer(int Id, Venta venta)
    {  
        return _ventasDataAccess.UpdateVenta(Id, venta); 
    }

    public Venta InsertVentaSer(Venta venta)
    {
         return _ventasDataAccess.InsertVenta(venta);
    }

    public bool DeleteVentaSer(int Id)
    {
        return _ventasDataAccess.DeleteVenta(Id);  
    }
}
