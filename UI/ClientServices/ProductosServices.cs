using Entidades;
using System.Net.Http;

namespace UI.ClientServices
{
    public class ProductosServices
    {
        private readonly HttpClient _httpClient;

        public ProductosServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Producto?> OneProductoSer(int Id)
        {
            return await _httpClient.GetFromJsonAsync<Producto>($"{Id}");
        }
       
        public async Task<List<Producto>?> GetProductosSer()
        {
            return await _httpClient.GetFromJsonAsync<List<Producto>>("");
        }

        public async Task UpdateProductoSer(int Id, Producto producto)
        {
            await _httpClient.PutAsJsonAsync($"{Id}", producto);
        }

        public async Task<Producto?> InsertProductoSer(Producto producto)
        {
            //return await _httpClient.PostAsJsonAsync<Usuario>("", usuario);
            if (producto == null)
            {
                return null; // Maneja el caso donde el usuario es null
            }

            // Realiza la solicitud POST
            var response = await _httpClient.PostAsJsonAsync("", producto);

            // Verifica si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Intenta leer el contenido como Usuario
                return await response.Content.ReadFromJsonAsync<Producto>();
            }

            return null; // Retorna null si la respuesta no fue exitosa
        }
        public async Task DeleteProductoSer(int Id)
        {
            await _httpClient.DeleteAsync($"{Id}");
        }
    }
}
