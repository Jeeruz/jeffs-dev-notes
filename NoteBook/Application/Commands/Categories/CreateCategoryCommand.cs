using MediatR;
using NoteBook.Data;
using NoteBook.Shared.DTOs;
using NoteBook.Shared.Common;
using NoteBook.Shared.Entities;

namespace NoteBook.Application.Commands.Categories
{
    // Marks this record as a MediatR write request expecting a CategoryResponse return type
    public record CreateCategoryCommand(CreateCategoryRequest Request) : IRequest<Result<CategoryResponse>>; 
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryResponse>>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public CreateCategoryCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<Result<CategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Map the incoming payload to the domain entity
                var Category = new Category
                {
                    Name = command.Request.Name,
                    Description = command.Request.Description
                };

                // Stage and persist the new record to the database asynchronously
                _db.Categories.Add(Category);
                await _db.SaveChangesAsync(cancellationToken);


                // Return flattened response DTO
                var response = new CategoryResponse
                {
                    Id = Category.Id,
                    Name = Category.Name,
                    Description = Category.Description,
                };

                return Result<CategoryResponse>.Success(response);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException?.Message ?? ex.Message;

                // Returning a Failure Result handles the error gracefully without re-throwing
                return Result<CategoryResponse>.Failure(message);
            }
        }
    }
}
