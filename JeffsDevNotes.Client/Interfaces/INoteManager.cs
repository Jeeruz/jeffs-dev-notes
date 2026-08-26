using JeffsDevNotes.Shared.DTOs;

namespace JeffsDevNotes.Client.Interfaces
{
    public interface INoteManager
    {
        Task<List<NoteResponse>> GetNotesAsync();
        Task<NoteResponse?> CreateNoteAsync(CreateNoteRequest request);
        Task<NoteResponse?> UpdateNoteAsync(CreateNoteRequest request);
        Task<bool> DeleteNoteAsync(int id); 
        Task<List<CategoryResponse>> GetCategoriesAsync();
        Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request);
        Task<CategoryResponse?> UpdateCategoryAsync(CreateCategoryRequest request);
        Task<bool> DeleteCategoryAsync(int id); 
    }
}
