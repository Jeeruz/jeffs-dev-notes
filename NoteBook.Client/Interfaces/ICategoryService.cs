using NoteBook.Shared.Common;
using NoteBook.Shared.DTOs;

namespace NoteBook.Client.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetCategoriesAsync();
        Task<Result<CategoryResponse>> CreateCategoryAsync(CreateCategoryRequest request);
        Task<Result<CategoryResponse>> UpdateCategoryAsync(int id, CreateCategoryRequest request);
        Task<Result<CategoryResponse>> DeleteCategoryAsync(int id);
    }
}
