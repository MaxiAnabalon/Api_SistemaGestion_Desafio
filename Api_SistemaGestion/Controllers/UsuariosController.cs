using Bussiness.Services;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly UsuariosServices _usuariosServices;
        public UsuariosController(ILogger<UsuariosController> logger, UsuariosServices productosServices)
        {
            _logger = logger;
            _usuariosServices = productosServices;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> GetUsuarios()
        {
            return _usuariosServices.GetUsuariosSer();
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            _logger.LogInformation($"Consultando por el producto con id: {id}");
            var usuario = _usuariosServices.OneUsuarioSer(id);
            if (usuario == null) { return NotFound(); }
            return usuario;
        }

        [HttpGet("filtrarPorNombreUsuario")]
        public ActionResult<List<Usuario>> GetUsuariosPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("El nombre no puede estar vacío.");
            }

            var usuariosFiltrados = _usuariosServices.GetUsuariosPorNombre(nombre);
            if (usuariosFiltrados == null || !usuariosFiltrados.Any())
            {
                return NotFound();
            }

            return Ok(usuariosFiltrados);
        }

        [HttpGet("{Usuario}/{Contra}")]
        public ActionResult<bool> ValidarIngresoUsuario(string Usuario, string Contra)
        {
            _logger.LogInformation($"Validando ingreso de usuario: {Usuario}");

            // Obtener la lista de todos los usuarios
            var usuarios = _usuariosServices.GetUsuariosSer();

            // Buscar el usuario en la lista
            Usuario usuarioEncontrado = null;

            foreach (var usuario in usuarios)
            {
                if (usuario.nombreDeUsuario.Equals(Usuario, StringComparison.OrdinalIgnoreCase))
                {
                    usuarioEncontrado = usuario;
                    break; // Salir del bucle si encontramos al usuario
                }
            }

            // Verificar si el usuario existe
            if (usuarioEncontrado == null)
            {
                return Unauthorized(); // Usuario no encontrado
            }

            // Validar la contraseña
            if (usuarioEncontrado.contraseña == Contra) // Aquí deberías usar un método de hash para comparación
            {
                return Ok(true); // Usuario y contraseña válidos
            }
            else
            {
                return Unauthorized(); // Contraseña incorrecta
            }

        }

        [HttpPost]
        public ActionResult<Usuario> CrearUsuario([FromBody] Usuario usuario) 
        {

            //var usuarioCreado = _usuariosServices.InsertUsuarioSer(usuario);
            //return CreatedAtAction(nameof(GetUsuario), new {id = usuarioCreado.Id}, usuario);

            // Validar el modelo
            if (usuario == null || !ModelState.IsValid)
            {
                return BadRequest("Los datos del usuario son inválidos."); // 400 Bad Request
            }

            // Obtener la lista de todos los usuarios
            var usuarios = _usuariosServices.GetUsuariosSer();

            // Verificar si el nombre de usuario ya existe
            foreach (var existingUser in usuarios)
            {
                if (existingUser.nombreDeUsuario.Equals(usuario.nombreDeUsuario, StringComparison.OrdinalIgnoreCase))
                {
                    return Conflict("El nombre de usuario ya está en uso."); // 409 Conflict
                }
            }

            try
            {
                var usuarioCreado = _usuariosServices.InsertUsuarioSer(usuario);
                return CreatedAtAction(nameof(GetUsuario), new { id = usuarioCreado.Id }, usuarioCreado);
            }
            catch (Exception ex)
            {
                // Log del error (asegúrate de tener un logger configurado)
                _logger.LogError(ex, "Error al crear el usuario.");
                return StatusCode(500, "Ocurrió un error al crear el usuario."); // 500 Internal Server Error
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> ModificarUsuario([FromRoute(Name = "id")] int id, [FromBody] Usuario usuario)
        {
            //_usuariosServices.UpdateUsuarioSer(id, usuario);
            //return NoContent();
            try
            {
                bool resultado = _usuariosServices.UpdateUsuarioSer(id, usuario);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la actualización fue exitosa
                }
                else
                {
                    return NotFound("Usuario no encontrado."); // 404 Not Found si el usuario no existe
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la operación de modificación: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno."); // 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Usuario> EliminarUsuario([FromRoute(Name ="id")] int id) 
        {
            //_usuariosServices.DeleteUsuarioSer(id);
            //return NoContent();
            try
            {
                bool resultado = _usuariosServices.DeleteUsuarioSer(id);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la eliminación fue exitosa
                }
                else
                {
                    return NotFound("Usuario no encontrado."); // 404 Not Found si el usuario no existe
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error
                Console.WriteLine($"Error en la operación de eliminación: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno."); // 500 Internal Server Error
            }
        }


        //[HttpPost]
        //public ActionResult<bool> ValidarIngresoUsuarioDos([FromBody] LoginRequestDTO request)
        //{
        //    _logger.LogInformation($"Validando ingreso de usuario: {request.nombreDeUsuario}");

        //    // Obtener la lista de todos los usuarios
        //    var usuarios = _usuariosServices.GetUsuariosSer();

        //    // Buscar el usuario en la lista
        //    Usuario usuarioEncontrado = null;

        //    foreach (var usuario in usuarios)
        //    {
        //        if (usuario.nombreDeUsuario.Equals(request.nombreDeUsuario, StringComparison.OrdinalIgnoreCase))
        //        {
        //            usuarioEncontrado = usuario;
        //            break; // Salir del bucle si encontramos al usuario
        //        }
        //    }

        //    // Verificar si el usuario existe
        //    if (usuarioEncontrado == null)
        //    {
        //        return Unauthorized(); // Usuario no encontrado
        //    }

        //    // Validar la contraseña
        //    if (usuarioEncontrado.contraseña == request.contraseña) // Aquí deberías usar un método de hash para comparación
        //    {
        //        return Ok(true); // Usuario y contraseña válidos
        //    }
        //    else
        //    {
        //        return Unauthorized(); // Contraseña incorrecta
        //    }
        //}

        //public class LoginRequestDTO
        //{
        //    [Required(ErrorMessage = "El campo nombre de usuario es requerido.")]
        //    [MaxLength(100, ErrorMessage = "La nombre de usuario no puede ser mayor a 100 caracteres.")]
        //    [MinLength(5, ErrorMessage = "La nombre de usuario no puede ser menor a 5 caracteres.")]
        //    [Display(Name = "Nombre de Usuario")]
        //    public string nombreDeUsuario { get; set; } = string.Empty;

        //    [Required(ErrorMessage = "El campo contraseña es requerido.")]
        //    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        //    [MaxLength(20, ErrorMessage = "La contraseña no puede ser mayor a 20 caracteres.")]
        //    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,20}$",
        //        ErrorMessage = "La contraseña debe contener al menos una letra mayúscula y un número.")]
        //    [Display(Name = "Contraseña")]
        //    public string contraseña { get; set; } = string.Empty;
        //}
    }

}
