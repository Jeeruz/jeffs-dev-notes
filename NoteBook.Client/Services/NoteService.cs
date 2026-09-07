using NoteBook.Client.Interfaces;
using NoteBook.Shared.Common;
using NoteBook.Shared.DTOs;
using System.Net.Http.Json;

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
        public async Task<Result<NoteResponse>> CreateNoteAsync(CreateNoteRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/note", request);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Result<NoteResponse>>();
                    return result ?? Result<NoteResponse>.Failure("Note not created");
                }
                else
                {
                    return Result<NoteResponse>.Failure("Note not created");
                }
            }
            catch(Exception ex)
            {
                return Result<NoteResponse>.Failure(ex.Message);
            }
        }

        // PUT: api/note/{id}
        // Routing: String interpolation inserts {id} into path (e.g., "api/note/5").
        // Controller Matching: NoteController.cs sees HTTP PUT -> matches [HttpPut("{id}")] attribute and binds {id} from URL to 'int id'.
        public async Task<Result<NoteResponse>> UpdateNoteAsync(int id, CreateNoteRequest request)
        {
            var response = await _http.PutAsJsonAsync($"api/note/{id}", request);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Result<NoteResponse>>();
                    return result ?? Result<NoteResponse>.Failure("Note not updated");
                }
                else
                {
                    return Result<NoteResponse>.Failure("Note not updated");
                }
            }
            catch (Exception ex)
            {
                return Result<NoteResponse>.Failure(ex.Message);
            }
        }

        // DELETE: api/note/{id}
        // Routing: Sends HTTP DELETE to "api/note/5".
        // Controller Matching: NoteController.cs sees HTTP DELETE -> matches [HttpDelete("{id}")] attribute and binds route segment to 'int id'.
        public async Task<Result<NoteResponse>> DeleteNoteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/note/{id}");
            try
            {
                var result = await response.Content.ReadFromJsonAsync<Result<NoteResponse>>();
                return result ?? Result<NoteResponse>.Failure("Note not deleted");
            }
            catch (Exception ex)
            {
                return Result<NoteResponse>.Failure(ex.Message);
            }
        }
        #endregion
    }
}