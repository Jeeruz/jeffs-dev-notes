using NoteBook.Shared.DTOs;

namespace NoteBook.Client.Interfaces
{
    public interface ICategory
    {
        Task<List<CategoryResponse>> GetCategoriesAsync();
        Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request);
        Task<CategoryResponse?> UpdateCategoryAsync(int id, CreateCategoryRequest request);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
