using NoteBook.Client.Interfaces;
using NoteBook.Shared.Common;
using System.Net.Http.Json;
using NoteBook.Shared.DTOs;

namespace NoteBook.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        // Injects HttpClient configured with the API backend base address (e.g., https://localhost:7001/)
        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        #region Category Operations

        // GET: api/category
        // Routing: Appends "api/category" to base URI.
        // Controller Matching: Strips "Controller" suffix from CategoryController.cs -> invokes [HttpGet] action.
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

        // POST: api/category
        // Routing: Sends HTTP POST with JSON body payload.
        // Controller Matching: CategoryController.cs handles payload via [HttpPost] CreateCategory([FromBody] CreateCategoryRequest request).
        public async Task<Result<CategoryResponse>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/category", request);
            try
            {             
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Result<CategoryResponse>>();
                    return result ?? Result<CategoryResponse>.Failure("Note not created");
                }
                else
                {
                    return Result<CategoryResponse>.Failure("Note not created");
                }
            }
            catch (Exception ex)
            {
                return Result<CategoryResponse>.Failure(ex.Message);
            }
        }

        // PUT: api/category/{id}
        // Routing: Passes {id} directly in URI path (e.g., "api/category/3").
        // Controller Matching: CategoryController.cs receives request -> matches [HttpPut("{id}")] -> maps URL path parameter to 'int id' and body to 'UpdateCategoryRequest request'.
        public async Task<Result<CategoryResponse>> UpdateCategoryAsync(int id, CreateCategoryRequest request)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/category/{id}", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Result<CategoryResponse>>();
                    return result ?? Result<CategoryResponse>.Failure("Category not updated");
                }
                else
                {
                    return Result<CategoryResponse>.Failure("Category not updated");
                }
            }
            catch (Exception ex)
            {
                return Result<CategoryResponse>.Failure(ex.Message);
            }
        }

        // DELETE: api/category/{id}
        // Routing: Sends HTTP DELETE call with path parameter (e.g., "api/category/3").
        // Controller Matching: CategoryController.cs matches [HttpDelete("{id}")] -> converts route segment '3' into method parameter 'int id'.
        public async Task<Result<CategoryResponse>> DeleteCategoryAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/category/{id}");
            try
            {
                var result = await response.Content.ReadFromJsonAsync<Result<CategoryResponse>>();
                return result ?? Result<CategoryResponse>.Failure("Category not deleted");
            }
            catch (Exception ex)
            {
                return Result<CategoryResponse>.Failure(ex.Message);
            }
        }
        #endregion
    }
}