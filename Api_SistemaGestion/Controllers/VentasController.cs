using Bussiness.Services;
using Entidades;
using Data.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;


namespace Api_SistemaGestion.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ILogger<VentasController> _logger;
        private readonly VentasServices _ventasServices;
        private readonly ProductosServices _productosServices;
        private readonly ProductosVendidosServices _productosVendidosServices; 

        public VentasController(ILogger<VentasController> logger, VentasServices ventasServices, ProductosServices productosServices,ProductosVendidosServices productosVendidosServices)
        {
            _logger = logger;
            _ventasServices = ventasServices;
            _productosServices = productosServices;
            _productosVendidosServices = productosVendidosServices;
        }

        //[HttpGet]
        //public ActionResult<List<Venta>> GetVentas()
        //{
        //    return _ventasServices.GetVentasSer();
        //}

        [HttpGet]
        public ActionResult<List<Venta>> GetVentas()
        {
            var ventas = _ventasServices.GetVentasSer();

            // Cargar la lista de productos vendidos para cada venta
            foreach (var venta in ventas)
            {
                venta.lsProductosVendidos = _productosVendidosServices.GetProductosVendidosPorVentaServices(venta.Id);
            }

            return ventas;
        }


        //[HttpGet("{id}")]
        //public ActionResult<Venta> GetVenta(int id)
        //{
        //    _logger.LogInformation($"Consultando por el producto con id: {id}");
        //    var venta = _ventasServices.OneVentaSer(id);
        //    if (venta == null) { return NotFound(); }
        //    return venta;
        //}

        [HttpGet("{id}")]
        public ActionResult<Venta> GetVenta(int id)
        {
            _logger.LogInformation($"Consultando por la venta con id: {id}");
            var venta = _ventasServices.OneVentaSer(id);
            if (venta == null) { return NotFound(); }

            // Cargar la lista de productos vendidos para la venta
            venta.lsProductosVendidos = _productosVendidosServices.GetProductosVendidosPorVentaServices(id);

            return venta;
        }

        [HttpPost]
        public ActionResult<Venta> CrearVenta([FromBody] Venta venta)
        {
            var ventaCreado = _ventasServices.InsertVentaSer(venta);
            var idDelUsuario = ventaCreado.IdUsuario;

            if (ventaCreado.lsProductosVendidos != null && ventaCreado.lsProductosVendidos.Count > 0)
            {
                // Lista para almacenar los productos vendidos
                List<ProductoVendido> productosVendidosParaInsertar = new List<ProductoVendido>();

                // Llenar la lista de productos vendidos
                foreach (var productoVendido in ventaCreado.lsProductosVendidos)
                {
                    // Asignar el IdVenta
                    productoVendido.IdVenta = ventaCreado.Id;

                    // Agregar a la lista temporal
                    productosVendidosParaInsertar.Add(productoVendido);

                    // Actualizar stock del producto
                    var producto = _productosServices.OneProductoSer(productoVendido.IdProducto);
                    producto.stock -= productoVendido.stock;
                    _productosServices.UpdateProductoSer(producto.Id, producto);
                }

                // Insertar todos los productos vendidos
                foreach (var productoVendido in productosVendidosParaInsertar)
                {
                    _productosVendidosServices.InsertProductosVendidoSer(productoVendido);
                }

            }

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

        //[HttpDelete("{id}")]
        //public ActionResult<Venta> EliminarVenta([FromRoute(Name = "id")] int id)
        //{
        //    try
        //    {
        //        bool resultado = _ventasServices.DeleteVentaSer(id);

        //        if (resultado)
        //        {
        //            return NoContent(); // 204 No Content si la eliminación fue exitosa
        //        }
        //        else
        //        {
        //            return NotFound("Venta no encontrada."); // 404 Not Found si el producto no existe
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Aquí puedes registrar el error
        //        Console.WriteLine($"Error en la operación de eliminación: {ex.Message}");
        //        return StatusCode(500, "Ocurrió un error interno."); // 500 Internal Server Error
        //    }
        //}

        [HttpDelete("{id}")]
        public ActionResult EliminarVentaCompleta(int id)
        {
            // Obtener la venta para eliminar
            var venta = _ventasServices.OneVentaSer(id);
            if (venta == null)
            {
                return NotFound(); // Si no se encuentra la venta
            }

            // Lista para almacenar los productos vendidos relacionados
            var productosVendidos = venta.lsProductosVendidos;

            // Actualizar el stock de los productos vendidos
            foreach (var productoVendido in productosVendidos)
            {
                var producto = _productosServices.OneProductoSer(productoVendido.IdProducto);
                producto.stock += productoVendido.stock; // Restablecer stock
                _productosServices.UpdateProductoSer(producto.Id, producto); // Actualizar el producto
            }

            // Eliminar los productos vendidos
            foreach (var productoVendido in productosVendidos)
            {
                _productosVendidosServices.DeleteProductoVendidoSer(productoVendido.Id); // Eliminar el registro
            }

            // Eliminar la venta
            _ventasServices.DeleteVentaSer(id);

            return NoContent(); // Retornar un estado 204 No Content
        }


    }
}
