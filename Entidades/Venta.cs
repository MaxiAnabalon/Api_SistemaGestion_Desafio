using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entidades;

public class Venta
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo comentario es requerido.")]
    [MaxLength(500, ErrorMessage = "El comentario no puede ser mayor a 500 caracteres.")]
    [MinLength(5, ErrorMessage = "El comentario no puede ser menor a 5 caracteres.")]
    [Display(Name = "Comentario")]
    public string comentario { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Id Usuario es requerido.")]
    [Range(1, double.MaxValue, ErrorMessage = "El Id Usuario debe ser mayor a 0.")]
    [Display(Name = "Id Usuario")]
    public int IdUsuario { get; set; }

    [NotMapped]
    public List<ProductoVendido>? lsProductosVendidos{ get; set; } = new List<ProductoVendido>();

    //public Venta(int id, string comentario, int idUsuario)
    //{
    //    this._Id = id;
    //    this._comentario = comentario;
    //    this._IdUsuario = idUsuario;
    //}

    //public int GetId() { return _Id; }
    //public string GetComentario() { return _comentario; }
    //public int GetIdUsuario() { return _IdUsuario; }

}

