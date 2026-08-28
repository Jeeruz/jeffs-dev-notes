using System.Net.Http.Json;
using NoteBook.Client.Interfaces;
using NoteBook.Shared.DTOs;

namespace NoteBook.Client.Services
{
    public class NoteService : INoteService
    {
        private readonly HttpClient _http;

        // Injects HttpClient configured with the API backend base address (e.g., https://localhost:7001/)
        public NoteService(HttpClient http)
        {
            _http = http;
        }

        #region Note Operations

        // GET: api/note
        // Routing: Appends "api/note" to the base URI.
        // Controller Matching: ASP.NET routes to NoteController.cs [HttpGet] endpoint with no parameters.
        public async Task<List<NoteResponse>> GetNotesAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<NoteResponse>>("api/note");
                return result ?? new List<NoteResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NoteManager Error]: {ex.Message}");
                return new List<NoteResponse>();
            }
        }

        // POST: api/note
        // Routing: Sends an HTTP POST to "api/note".
        // Controller Matching: NoteController.cs sees HTTP POST -> maps body payload to [HttpPost] CreateNote([FromBody] CreateNoteRequest request).
        public async Task<NoteResponse?> CreateNoteAsync(CreateNoteRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/note", request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<NoteResponse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NoteManager Error]: {ex.Message}");
            }

            return null;
        }

        // PUT: api/note/{id}
        // Routing: String interpolation inserts {id} into path (e.g., "api/note/5").
        // Controller Matching: NoteController.cs sees HTTP PUT -> matches [HttpPut("{id}")] attribute and binds {id} from URL to 'int id'.
        public async Task<NoteResponse?> UpdateNoteAsync(int id, CreateNoteRequest request)
        {
            try
            {
                // Updated from PostAsJsonAsync to PutAsJsonAsync to match standard HTTP PUT endpoints
                var response = await _http.PutAsJsonAsync($"api/note/{id}", request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<NoteResponse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NoteManager Error]: {ex.Message}");
            }

            return null;
        }

        // DELETE: api/note/{id}
        // Routing: Sends HTTP DELETE to "api/note/5".
        // Controller Matching: NoteController.cs sees HTTP DELETE -> matches [HttpDelete("{id}")] attribute and binds route segment to 'int id'.
        public async Task<bool> DeleteNoteAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/note/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NoteManager Error]: {ex.Message}");
            }

            return false;
        }

        #endregion
    }
}