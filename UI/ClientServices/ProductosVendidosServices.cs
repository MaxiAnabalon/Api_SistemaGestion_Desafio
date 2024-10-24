using Entidades;
using UI.Components.Pages;

namespace UI.ClientServices
{
    public class ProductosVendidosServices
    {
        private readonly HttpClient _httpClient;

        public ProductosVendidosServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ProductoVendido?> OneProductoVendidoSer(int Id)
        {
            return await _httpClient.GetFromJsonAsync<ProductoVendido>($"{Id}");
        }
        public async Task<List<ProductoVendido>?> GetProductosVendidosSer()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoVendido>>("");
        }

        public async Task<List<ProductoVendido>?> GetProductosVendidosPorVentaServices(int ventaID)
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoVendido>>($"{ventaID}");
        }

        public async Task UpdateProductoVendidoSer(int Id, ProductoVendido productoVend)
        {
            await _httpClient.PutAsJsonAsync($"{Id}", productoVend);
        }

        public async Task<ProductoVendido?> InsertProductosVendidoSer(ProductoVendido productoVend)
        {
            //return await _httpClient.PostAsJsonAsync<Usuario>("", usuario);
            if (productoVend == null)
            {
                return null; // Maneja el caso donde el usuario es null
            }

            // Realiza la solicitud POST
            var response = await _httpClient.PostAsJsonAsync("", productoVend);

            // Verifica si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Intenta leer el contenido como Usuario
                return await response.Content.ReadFromJsonAsync<ProductoVendido>();
            }

            return null; // Retorna null si la respuesta no fue exitosa
        }
        public async Task DeleteProductoVendidoSer(int Id)
        {
            await _httpClient.DeleteAsync($"{Id}");
        }
    }
}
