using Bussiness.Services;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_SistemaGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly ProductosServices _productosServices;
        public ProductosController(ILogger<ProductosController> logger, ProductosServices productosServices)
        { _logger = logger;
            _productosServices = productosServices;
        }

        [HttpGet]
        public ActionResult<List<Producto>> GetProductos()
        {
            return _productosServices.GetProductosSer();
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            _logger.LogInformation($"Consultando por el producto con id: {id}");
            var producto = _productosServices.OneProductoSer(id);
            if (producto == null) { return NotFound(); }
            return producto;
        }



        [HttpPost]
        public ActionResult<Producto> CrearProducto([FromBody] Producto producto)
        {
            var productoCreado = _productosServices.InsertProductoSer(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = productoCreado.Id }, producto);
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> ModificarProducto([FromRoute(Name = "id")] int id, [FromBody] Producto producto)
        {
            try
            {
                bool resultado = _productosServices.UpdateProductoSer(id, producto);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la actualización fue exitosa
                }
                else
                {
                    return NotFound("Producto no encontrado."); // 404 Not Found si el producto no existe
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la operación de modificación: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno."); // 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Producto> EliminarProducto([FromRoute(Name = "id")] int id)
        {
            try
            {
                bool resultado = _productosServices.DeleteProductoSer(id);

                if (resultado)
                {
                    return NoContent(); // 204 No Content si la eliminación fue exitosa
                }
                else
                {
                    return NotFound("Producto no encontrado."); // 404 Not Found si el producto no existe
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
