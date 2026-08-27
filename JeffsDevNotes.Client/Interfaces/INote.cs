using JeffsDevNotes.Shared.DTOs;

namespace JeffsDevNotes.Client.Interfaces
{
    public interface INote
    {
        Task<List<NoteResponse>> GetNotesAsync();
        Task<NoteResponse?> CreateNoteAsync(CreateNoteRequest request);
        Task<NoteResponse?> UpdateNoteAsync(int id, CreateNoteRequest request);
        Task<bool> DeleteNoteAsync(int id); 
    }
}
