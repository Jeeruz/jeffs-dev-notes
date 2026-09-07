using MediatR;
using NoteBook.Data;
using NoteBook.Shared.DTOs;
using NoteBook.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace NoteBook.Application.Commands.Notes
{
    // Marks this record as a MediatR write request expecting a NoteResponse return type
    public record UpdateNoteCommand(int id, CreateNoteRequest Request) : IRequest<Result<NoteResponse>>;
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Result<NoteResponse>>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public UpdateNoteCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<Result<NoteResponse>> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var Note = await _db.Notes.SingleAsync(forDeletetion => forDeletetion.Id == command.Request.Id);
                Note.Content = command.Request.Content;
                Note.Description = command.Request.Description;
                Note.CategoryId = command.Request.CategoryId;

                await _db.SaveChangesAsync();

                // Fetch related entity data to complete the response contract
                var category = await _db.Categories.FindAsync(new object[] { Note.CategoryId }, cancellationToken);

                // Return flattened response DTO
                var response = new NoteResponse
                {
                    Id = Note.Id,
                    Content = Note.Content,
                    Description = Note.Description,
                    CategoryId = Note.CategoryId,
                    CategoryName = category?.Name ?? string.Empty,
                };

                return Result<NoteResponse>.Success(response);
            }
            catch(Exception ex)
            {
                string message = ex.InnerException?.Message ?? ex.Message;
                return Result<NoteResponse>.Failure(message);
            } 
        }
    }
}
