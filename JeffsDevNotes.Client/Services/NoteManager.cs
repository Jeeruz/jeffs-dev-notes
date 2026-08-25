using System.Net.Http.Json;
using JeffsDevNotes.Client.Interfaces;
using JeffsDevNotes.Shared.DTOs;


namespace JeffsDevNotes.Client.Services
{
    public class NoteManager : INoteManager
    {
        private readonly HttpClient _http;

        public NoteManager(HttpClient http)
        {
            _http = http;
        }

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

        public async Task<List<CategoryResponse>> GetCategoriesAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<CategoryResponse>>("api/category");
                return result ?? new List<CategoryResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NoteManager Error]: {ex.Message}");
                return new List<CategoryResponse>();
            }
        }

        public async Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/category", request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CategoryResponse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NoteManager Error]: {ex.Message}");
            }

            return null;
        }
    }
}
