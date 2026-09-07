using MediatR;
using NoteBook.Data;
using NoteBook.Shared.DTOs;
using NoteBook.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace NoteBook.Application.Commands.Categories
{
    // Marks this record as a MediatR write request expecting a CategoryResponse return type
    public record DeleteCategoryCommand(int id) : IRequest<Result<CategoryResponse>>;
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<CategoryResponse>>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public DeleteCategoryCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<Result<CategoryResponse>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Map the incoming payload to the domain entity
                var Category = await _db.Categories.SingleAsync(forDeletetion => forDeletetion.Id == command.id);

                // Stage and persist the new record to the database asynchronously
                _db.Categories.Remove(Category);
                var status = await _db.SaveChangesAsync(cancellationToken);

                // Return flattened response DTO
                var response = new CategoryResponse();

                return Result<CategoryResponse>.Success(response);
            }
            catch(Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                return Result<CategoryResponse>.Failure(message);
            }
        }
    }
}
