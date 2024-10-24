using System.ComponentModel.DataAnnotations;

namespace Entidades;

public class ProductoVendido
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Id Producto es requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "El Id Producto debe ser mayor a 0.")]
    [Display(Name = "Id Producto")]
    public int IdProducto { get; set; }

    [Required(ErrorMessage = "El campo stock es requerido.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
    [Display(Name = "Stock")]
    public int stock { get; set; }

    [Required(ErrorMessage = "El campo Id Venta es requerido.")]
    [Range(0, int.MaxValue, ErrorMessage = "El Id Venta debe ser mayor a 0.")]
    [Display(Name = "Id Venta")]
    public int IdVenta { get; set; }


    //public ProductoVendido(int id, int idProducto, int stock, int IdVenta)
    //{
    //    this._Id = id;
    //    this._IdProducto = idProducto;
    //    this._stock = stock;
    //    this._IdVenta = IdVenta;
    //}

    //public int GetId() { return _Id; }
    //public int GetIdProducto() { return _IdProducto; }
    //public double GetStock() { return _stock; }
    //public int GetIdVenta() { return _IdVenta; }
}

