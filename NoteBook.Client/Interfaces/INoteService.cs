using NoteBook.Shared.DTOs;

namespace NoteBook.Client.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteResponse>> GetNotesAsync();
        Task<NoteResponse?> CreateNoteAsync(CreateNoteRequest request);
        Task<NoteResponse?> UpdateNoteAsync(int id, CreateNoteRequest request);
        Task<bool> DeleteNoteAsync(int id); 
    }
}
