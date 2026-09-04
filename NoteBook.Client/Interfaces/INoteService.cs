using NoteBook.Shared.DTOs;
using NoteBook.Shared.Common;

namespace NoteBook.Client.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteResponse>> GetNotesAsync();
        Task<Result<NoteResponse>> CreateNoteAsync(CreateNoteRequest request);
        Task<Result<NoteResponse>> UpdateNoteAsync(int id, CreateNoteRequest request);
        Task<Result<NoteResponse>> DeleteNoteAsync(int id); 
    }
}
