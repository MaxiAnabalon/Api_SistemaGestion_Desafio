using Entidades;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace UI.ClientServices
{
    public class UsuariosServices
    {
        private readonly HttpClient _httpClient;

        public UsuariosServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidarIngresoUsuarioAsync(string usuario, string contra)
        {
            var response = await _httpClient.GetAsync($"{usuario}/{contra}");
           
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
           
            // Si la respuesta no fue exitosa, puedes lanzar una excepción o manejar el error como desees
            throw new Exception("Usuario o contraseña incorrecta");
        }

        public async Task<Usuario?> OneUsuarioSer(int Id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"{Id}");
        }
        public async Task<List<Usuario>?> GetUsuariosSer()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("");
        }

        public async Task<List<Usuario>?> GetUsuariosPorNombreUsuario(string nombreUsuario)
        {
            // Asegúrate de que el nombre no esté vacío o nulo
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return null; // O maneja el caso según tus necesidades
            }
            var baseUrl = _httpClient.BaseAddress; 
            var url = $"{baseUrl}filtrarPorNombreUsuario?nombre={nombreUsuario}";
            return await _httpClient.GetFromJsonAsync<List<Usuario>>(url);

            //return await _httpClient.GetFromJsonAsync<List<Usuario>>(
            //   QueryHelpers.AddQueryString("",new Dictionary<string, string>() { { "filtrarPorNombreUsuario", nombreUsuario} }));
        }

        //public async Task<bool> UpdateUsuarioSer(int Id, Usuario usuario)
        public async Task UpdateUsuarioSer(int Id, Usuario usuario)
        {
            await _httpClient.PutAsJsonAsync($"{Id}", usuario);

            //if (usuario == null)
            //{
            //    return false; // Maneja el caso donde usuario es null
            //}

            //// Realiza la solicitud PUT
            //var response = await _httpClient.PutAsJsonAsync($"{Id}", usuario);

            //// Verifica si la respuesta fue exitosa
            //if (response.IsSuccessStatusCode)
            //{
            //    // Intenta leer el contenido como booleano
            //    return await response.Content.ReadFromJsonAsync<bool>();
            //}

            //return false; // Si la respuesta no fue exitosa
        }

        //public async Task<Usuario?> InsertUsuarioSer(Usuario usuario)
        //{
        //    //return await _httpClient.PostAsJsonAsync<Usuario>("", usuario);
        //    if (usuario == null)
        //    {
        //        return null; // Maneja el caso donde el usuario es null
        //    }

        //    // Realiza la solicitud POST
        //    var response = await _httpClient.PostAsJsonAsync("", usuario);

        //    // Verifica si la respuesta fue exitosa
        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Intenta leer el contenido como Usuario
        //        return await response.Content.ReadFromJsonAsync<Usuario>();
        //    }

        //    return null; // Retorna null si la respuesta no fue exitosa
        //}
        public async Task<(Usuario? usuario, string mensaje)> InsertUsuarioSer(Usuario usuario)
        {
            if (usuario == null)
            {
                return (null, "El usuario no es válido."); // Maneja el caso donde el usuario es null
            }

            // Realiza la solicitud POST
            var response = await _httpClient.PostAsJsonAsync("", usuario);

            // Verifica si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Intenta leer el contenido como Usuario
                var usuarioCreado = await response.Content.ReadFromJsonAsync<Usuario>();
                return (usuarioCreado, "Usuario creado exitosamente.");
            }

            // Aquí puedes extraer el mensaje de error de la respuesta
            var errorMessage = await response.Content.ReadAsStringAsync();
            return (null, errorMessage); // Retorna null y el mensaje de error si la respuesta no fue exitosa
        }

        //public async Task InsertUsuarioSer(Usuario usuario)
        //{
        //     await _httpClient.PostAsJsonAsync("",usuario);
        //}
        public async Task DeleteUsuarioSer(int Id)
        {
            //return await _httpClient.DeleteFromJsonAsync<bool>($"{Id}");
            await _httpClient.DeleteAsync($"{Id}");
        }
    }
}
