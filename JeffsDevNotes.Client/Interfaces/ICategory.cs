using JeffsDevNotes.Shared.DTOs;

namespace JeffsDevNotes.Client.Interfaces
{
    public interface ICategory
    {
        Task<List<CategoryResponse>> GetCategoriesAsync();
        Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request);
        Task<CategoryResponse?> UpdateCategoryAsync(int id, CreateCategoryRequest request);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
