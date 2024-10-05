using Bussiness.Services;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ILogger<VentasController> _logger;
        private readonly VentasServices _ventasServices;
        public VentasController(ILogger<VentasController> logger, VentasServices ventasServices)
        {
            _logger = logger;
            _ventasServices = ventasServices;
        }

        [HttpGet]
        public ActionResult<List<Venta>> GetVentas()
        {
            return _ventasServices.GetVentasSer();
        }

        [HttpGet("{id}")]
        public ActionResult<Venta> GetVenta(int id)
        {
            _logger.LogInformation($"Consultando por el producto con id: {id}");
            var venta = _ventasServices.OneVentaSer(id);
            if (venta == null) { return NotFound(); }
            return venta;
        }

        [HttpPost]
        public ActionResult<Venta> CrearVenta([FromBody] Venta venta)
        {
            var ventaCreado = _ventasServices.InsertVentaSer(venta);
            return CreatedAtAction(nameof(GetVenta), new { id = ventaCreado.Id }, venta);
        }

        [HttpPut("{id}")]
        public ActionResult<Venta> ModificarVenta([FromRoute(Name = "id")] int id, [FromBody] Venta venta)
        {
            try
            {
                bool resultado = _ventasServices.UpdateVentaSer(id, venta);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la actualización fue exitosa
                }
                else
                {
                    return NotFound("Venta no encontrado."); // 404 Not Found si el producto no existe
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la operación de modificación: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno."); // 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Venta> EliminarVenta([FromRoute(Name = "id")] int id)
        {
            try
            {
                bool resultado = _ventasServices.DeleteVentaSer(id);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la eliminación fue exitosa
                }
                else
                {
                    return NotFound("Venta no encontrada."); // 404 Not Found si el producto no existe
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
