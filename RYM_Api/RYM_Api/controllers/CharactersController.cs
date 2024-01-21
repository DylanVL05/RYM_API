using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RYM_Api.models;

namespace RYM_Api.controllers
{
    public class CharactersController
    {
        private readonly HttpClient client;

        public CharactersController()
        {
            client = new HttpClient();
        }

        public async Task<Characters> GetAllCharacters() 
        {
            try
            {
                // Realiza la solicitud GET a la API de Rick and Morty
                HttpResponseMessage response = await client.GetAsync("https://rickandmortyapi.com/api/character");
                response.EnsureSuccessStatusCode();

                // Lee el contenido de la respuesta como una cadena JSON
                string responseJson = await response.Content.ReadAsStringAsync();

                // Deserializa la cadena JSON en un objeto Characters
                Characters characters = JsonConvert.DeserializeObject<Characters>(responseJson);

                // Devuelve el objeto Characters
                return characters;
            }
            catch (HttpRequestException ex)
            {
                // Maneja excepciones relacionadas con la solicitud HTTP
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                // Maneja excepciones relacionadas con la deserialización JSON
                Console.WriteLine($"Error al deserializar JSON: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Maneja otras excepciones no previstas
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;

            }
        }
    }
}
