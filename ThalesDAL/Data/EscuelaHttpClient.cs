using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using ThalesDTO.Models;

namespace ThalesDAL.Data
{
    public interface IEscuelaHttpClient
    {
       public Task<T?> GetAsync<T>(string url);
    }

    public class EscuelaHttpClient(HttpClient httpClient) : IEscuelaHttpClient
    {
        private static readonly string ApiURL = "https://api.escuelajs.co/api/v1";

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            if (string.IsNullOrEmpty(endpoint))
                throw new ArgumentException("El endpoint no puede ser nulo o vacío.", nameof(endpoint));
            try
            {
                var response = await httpClient.GetAsync($"{ApiURL}/{endpoint}");
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                return default;
            }
        }
    }
}
