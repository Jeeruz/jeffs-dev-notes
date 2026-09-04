using NoteBook.Data;
using NoteBook.Shared.DTOs;
using NoteBook.Shared.Common;
using NoteBook.Shared.Entities;
using MediatR;

namespace NoteBook.Application.Commands
{
    // Marks this record as a MediatR write request expecting a NoteResponse return type
    public record CreateNoteCommand(CreateNoteRequest Request) : IRequest<Result<NoteResponse>>;
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Result<NoteResponse>>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public CreateNoteCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<Result<NoteResponse>> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            try
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
                var response = new NoteResponse
                {
                    Id = note.Id,
                    Content = note.Content,
                    Description = note.Description,
                    CategoryId = note.CategoryId,
                    CategoryName = category?.Name ?? string.Empty,
                };

                return Result<NoteResponse>.Success(response);
            }
            catch(Exception ex)
            {
                string message = ex.InnerException?.Message ?? ex.Message;

                // Returning a Failure Result handles the error gracefully without re-throwing
                return Result<NoteResponse>.Failure(message);
            }
        }
    }
}
