using MediatR;
using Microsoft.EntityFrameworkCore;
using NoteBook.Data;
using NoteBook.Shared.Common;
using NoteBook.Shared.DTOs;
namespace NoteBook.Application.Commands
{
    // Marks this record as a MediatR write request expecting a CategoryResponse return type
    public record UpdateCategoryCommand(int id, CreateCategoryRequest Request) : IRequest<Result<CategoryResponse>>;
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryResponse>>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public UpdateCategoryCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<Result<CategoryResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var Category = await _db.Categories.SingleAsync(forDeletetion => forDeletetion.Id == command.id);
                Category.Name = command.Request.Name;
                Category.Description = command.Request.Description;

                await _db.SaveChangesAsync();

                var response = new CategoryResponse
                {
                    Id = Category.Id,
                    Name = Category.Name,
                    Description = Category.Description,
                };

                // Return flattened response DTO
                return Result<CategoryResponse>.Success(response);
            }
            catch(Exception ex)
            {
                string message = ex.InnerException?.Message ?? ex.Message;
                return Result<CategoryResponse>.Failure(message);
            }
        }
    }
}
