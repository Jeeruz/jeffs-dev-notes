using JeffsDevNotes.Shared.DTOs;

namespace JeffsDevNotes.Client.Interfaces
{
    public interface INoteManager
    {
        Task<List<NoteResponse>> GetNotesAsync();
        Task<NoteResponse?> CreateNoteAsync(CreateNoteRequest request);
        Task<List<CategoryResponse>> GetCategoriesAsync();
        Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request);
    }
}
