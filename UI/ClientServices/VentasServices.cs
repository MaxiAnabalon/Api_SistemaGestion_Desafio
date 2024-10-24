using Entidades;
using System.Net.Http;

namespace UI.ClientServices
{
    public class VentasServices
    {
        private readonly HttpClient _httpClient;
        public VentasServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Venta?> OneVentaSer(int Id)
        {
            return await _httpClient.GetFromJsonAsync<Venta>($"{Id}");

        }
        public async Task<List<Venta>?> GetVentasSer()
        {
            return await _httpClient.GetFromJsonAsync<List<Venta>>("");
        }

        public async Task UpdateVentaSer(int Id, Venta venta)
        {
            await _httpClient.PutAsJsonAsync($"{Id}", venta);
        }

        public async Task<Venta?> InsertVentaSer(Venta venta)
        {
            //return await _httpClient.PostAsJsonAsync<Usuario>("", usuario);
            if (venta == null)
            {
                return null; // Maneja el caso donde el usuario es null
            }

            // Realiza la solicitud POST
            var response = await _httpClient.PostAsJsonAsync("", venta);

            // Verifica si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Intenta leer el contenido como Usuario
                return await response.Content.ReadFromJsonAsync<Venta>();
            }

            return null; // Retorna null si la respuesta no fue exitosa
        }

        public async Task DeleteVentaSer(int Id)
        {
            await _httpClient.DeleteAsync($"{Id}");
        }


        public async Task<Venta> CrearVentaAsync(Venta venta)
        {
            var response = await _httpClient.PostAsJsonAsync("", venta);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Venta>();
            }
            else
            {
                // Manejar el error según sea necesario
                throw new Exception($"Error al crear la venta: {response.ReasonPhrase}");
            }
        }
    }
}
