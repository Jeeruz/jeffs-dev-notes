using JeffsDevNotes.Data;
using JeffsDevNotes.Shared;
using JeffsDevNotes.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JeffsDevNotes.Application.Commands.Notes
{
    // Marks this record as a MediatR write request expecting a NoteResponse return type
    public record UpdateNoteCommand(int id, CreateNoteRequest Request) : IRequest<NoteResponse>;
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteResponse>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public UpdateNoteCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<NoteResponse> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            var Note = await _db.Notes.SingleAsync(forDeletetion => forDeletetion.Id == command.Request.Id);
            Note.Content = command.Request.Content;
            Note.Description = command.Request.Description;
            Note.CategoryId = command.Request.CategoryId;

            await _db.SaveChangesAsync();

            // Fetch related entity data to complete the response contract
            var category = await _db.Categories.FindAsync(new object[] { Note.CategoryId }, cancellationToken);

            // Return flattened response DTO
            return new NoteResponse
            {
                Id = Note.Id,
                Content = Note.Content,
                Description = Note.Description,
                CategoryId = Note.CategoryId,
                CategoryName = category?.Name ?? string.Empty,
            };
        }
    }
}
