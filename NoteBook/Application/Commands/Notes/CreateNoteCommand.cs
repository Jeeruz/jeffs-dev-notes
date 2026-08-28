using NoteBook.Data;
using NoteBook.Shared;
using NoteBook.Shared.DTOs;
using MediatR;

namespace NoteBook.Application.Commands
{
    // Marks this record as a MediatR write request expecting a NoteResponse return type
    public record CreateNoteCommand(CreateNoteRequest Request) : IRequest<NoteResponse>;
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteResponse>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public CreateNoteCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<NoteResponse> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            // Map the incoming payload to the domain entity
            var note = new Note
            {
                Content = command.Request.Content,
                Description = command.Request.Description,
                CategoryId = command.Request.CategoryId
            };

            // Stage and persist the new record to the database asynchronously
            _db.Notes.Add(note);
            await _db.SaveChangesAsync(cancellationToken);

            // Fetch related entity data to complete the response contract
            var category = await _db.Categories.FindAsync(new object[] { note.CategoryId }, cancellationToken);

            // Return flattened response DTO
            return new NoteResponse
            {
                Id = note.Id,
                Content = note.Content,
                Description = note.Description,
                CategoryId = note.CategoryId,
                CategoryName = category?.Name ?? string.Empty,
            };
        }
    }
}
