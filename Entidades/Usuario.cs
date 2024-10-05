using System.ComponentModel.DataAnnotations;

namespace Entidades;
public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo nombre es requerido.")]
    [MaxLength(100, ErrorMessage = "La nombre no puede ser mayor a 100")]
    [MinLength(5, ErrorMessage = "La nombre no puede ser menor a 5")]
    [Display(Name = "Nombre")]
    public string nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo apellido es requerido.")]
    [MaxLength(100, ErrorMessage = "La apellido no puede ser mayor a 100")]
    [MinLength(5, ErrorMessage = "La apellido no puede ser menor a 5")]
    [Display(Name = "Apellido")]
    public string apellido { get; set; }= string.Empty;

    [Required(ErrorMessage = "El campo nombre de usuario es requerido.")]
    [MaxLength(100, ErrorMessage = "La nombre de usuario no puede ser mayor a 100")]
    [MinLength(5, ErrorMessage = "La nombre de usuario no puede ser menor a 5")]
    [Display(Name = "Nombre de Usuario")]
    public string nombreDeUsuario { get; set; }=string.Empty;

    [Required(ErrorMessage = "El campo contraseña es requerido.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(20, ErrorMessage = "La contraseña no puede ser mayor a 20 caracteres.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,20}$",
    ErrorMessage = "La contraseña debe contener al menos una letra mayúscula y un número.")]
    [Display(Name = "Contraseña")]
    public string contraseña { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo email es requerido.")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
    [MaxLength(100, ErrorMessage = "El email no puede ser mayor a 100 caracteres.")]
    [Display(Name = "Email")]
    public string email { get; set; } = string.Empty;   

    //public Usuario(int id, string nombre, string apellido, string nombreDeUsuario, string contraseña, string email)
    //{
    //    this._id = id;
    //    this._nombre = nombre;
    //    this._apellido = apellido;
    //    this._nombreDeUsuario = nombreDeUsuario;
    //    this._contraseña = contraseña;
    //    this._email = email;
    //}

    //public int GetId() { return _id; }

    //public string GetNombre() { return _nombre; }
    //public string GetApellido() { return _apellido; }

    //public string GetNombreUsuario() { return _nombreDeUsuario; }
    //public string GetContraseña() { return _contraseña; }

    //public string GetEmail() { return _email; }
}

