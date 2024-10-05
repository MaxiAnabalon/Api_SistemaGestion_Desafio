using Bussiness.Services;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public ActionResult<Usuario> CrearUsuario([FromBody] Usuario usuario) 
        {
            var usuarioCreado = _usuariosServices.InsertUsuarioSer(usuario);
            return CreatedAtAction(nameof(GetUsuario), new {id = usuarioCreado.Id}, usuario);
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
    }
}
