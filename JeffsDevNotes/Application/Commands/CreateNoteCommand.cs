using JeffsDevCategorys.Application.Commands;
using JeffsDevNotes.Data;
using JeffsDevNotes.Shared;
using JeffsDevNotes.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JeffsDevNotes.Application.Commands
{
    // Marks this record as a MediatR write request expecting a NoteResponse return type
    public record CreateNoteCommand(CreateNoteRequest Request) : IRequest<NoteResponse>;
    public record UpdateNoteCommand(CreateNoteRequest Request) : IRequest<NoteResponse>;
    public record DeleteNoteCommand(CreateNoteRequest Request) : IRequest<NoteResponse>;

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

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public UpdateCategoryCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var Category = await _db.Categories.SingleAsync(forDeletetion => forDeletetion.Id == command.Request.Id);
            Category.Name = command.Request.Name;
            Category.Description = command.Request.Description;

            await _db.SaveChangesAsync();

            // Return flattened response DTO
            return new CategoryResponse
            {
                Id = Category.Id,
                Name = Category.Name,
                Description = Category.Description,
            };
        }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteNoteCommand, NoteResponse>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public DeleteCategoryCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<NoteResponse> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
        {
            // Map the incoming payload to the domain entity
            var Note = await _db.Notes.SingleAsync(forDeletetion => forDeletetion.Id == command.Request.Id);

            // Stage and persist the new record to the database asynchronously
            _db.Notes.Remove(Note);
            var status = await _db.SaveChangesAsync(cancellationToken);

            // Return flattened response DTO
            return new NoteResponse();
        }
    }
}
