using System.ComponentModel.DataAnnotations;

namespace Entidades;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo descripción es requerido.")]
    [MaxLength(250, ErrorMessage = "La descripción no puede ser mayor a 250")]
    [MinLength(5, ErrorMessage = "La descripción no puede ser menor a 5")]
    [Display(Name = "Descripción")]
    public string descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage ="El campo costo es requerido")]
    [Range(0,double.MaxValue, ErrorMessage ="El costo debe ser mayor o igual a 0")]
    [Display(Name ="Costo")]
    public double costo { get; set; }
    [Required(ErrorMessage = "El campo precio de venta es requerido")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor o igual a 0")]
    [Display(Name = "Precio de Venta")]
    public double precioVenta { get; set; }

    [Required(ErrorMessage = "El campo stock es requerido")]
    [Range(0, double.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
    [Display(Name = "Stock")]
    public int stock { get; set; }

    [Required(ErrorMessage = "El campo Id Usuario es requerido")]
    [Range(0, double.MaxValue, ErrorMessage = "El Id Usuario debe ser mayor o igual a 0")]
    [Display(Name = "Id Usuario")]
    public int IdUsuario { get; set; }

    //public Producto(int id, string descripcion, double costo, double precioVenta, int stock, int idUsuario)
    //{
    //    this._Id = id;
    //    this._descripcion = descripcion;
    //    this._costo = costo;
    //    this._precioVenta = precioVenta;
    //    this._stock = stock;
    //    this._IdUsuario = idUsuario;
    //}

    //public int GetId() { return _Id; }
    //public string GetDescripcion() { return _descripcion; }
    //public double GetCosto() { return _costo; }
    //public double GetPrecioVenta() { return _precioVenta; }
    //public int GetStock() { return _stock; }
    //public int GetIdUsuario() { return _IdUsuario; }
}

