using NoteBook.Data;
using NoteBook.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NoteBook.Application.Commands
{
    // Marks this record as a MediatR write request expecting a NoteResponse return type
    public record DeleteNoteCommand(int id) : IRequest<NoteResponse>;
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, NoteResponse>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public DeleteNoteCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<NoteResponse> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
        {
            // Map the incoming payload to the domain entity
            var Note = await _db.Notes.SingleAsync(forDeletetion => forDeletetion.Id == command.id);

            // Stage and persist the new record to the database asynchronously
            _db.Notes.Remove(Note);
            var status = await _db.SaveChangesAsync(cancellationToken);

            // Return flattened response DTO
            return new NoteResponse();
        }
    }
}
