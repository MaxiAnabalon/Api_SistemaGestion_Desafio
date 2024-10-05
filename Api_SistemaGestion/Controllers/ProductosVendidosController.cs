using Bussiness.Services;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosVendidosController : ControllerBase
    {
        private readonly ILogger<ProductosVendidosController> _logger;
        private readonly ProductosVendidosServices _productosVendidosServices;
        public ProductosVendidosController(ILogger<ProductosVendidosController> logger, ProductosVendidosServices productosVendidosServices)
        {
            _logger = logger;
            _productosVendidosServices = productosVendidosServices;
        }

        [HttpGet]
        public ActionResult<List<ProductoVendido>> GetProductosVendidos()
        {
            return _productosVendidosServices.GetProductosVendidosSer();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductoVendido> GetProductoVendido(int id)
        {
            _logger.LogInformation($"Consultando por el producto vendido con id: {id}");
            var productoVendidos = _productosVendidosServices.OneProductoVendidoSer(id);
            if (productoVendidos == null) { return NotFound(); }
            return productoVendidos;
        }



        [HttpPost]
        public ActionResult<ProductoVendido> CrearProductoVendido([FromBody] ProductoVendido productoVendidos)
        {
            var productoVendidoCreado = _productosVendidosServices.InsertProductosVendidoSer(productoVendidos);
            return CreatedAtAction(nameof(GetProductoVendido), new { id = productoVendidoCreado.Id }, productoVendidos);
        }

        [HttpPut("{id}")]
        public ActionResult<ProductoVendido> ModificarProductoVendido([FromRoute(Name = "id")] int id, [FromBody] ProductoVendido productoVendido)
        {
            try
            {
                bool resultado = _productosVendidosServices.UpdateProductoVendidoSer(id, productoVendido);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la actualización fue exitosa
                }
                else
                {
                    return NotFound("Producto vendido no encontrado."); // 404 Not Found si el producto no existe
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la operación de modificación: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno."); // 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<ProductoVendido> EliminarProductoVendido([FromRoute(Name = "id")] int id)
        {
            try
            {
                bool resultado = _productosVendidosServices.DeleteProductoVendidoSer(id);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la eliminación fue exitosa
                }
                else
                {
                    return NotFound("Producto vendido no encontrado."); // 404 Not Found si el producto no existe
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
