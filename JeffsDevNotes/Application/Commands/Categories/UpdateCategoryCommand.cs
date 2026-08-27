using MediatR;
using JeffsDevNotes.Data;
using JeffsDevNotes.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
namespace JeffsDevCategorys.Application.Commands
{
    // Marks this record as a MediatR write request expecting a CategoryResponse return type
    public record UpdateCategoryCommand(int id, CreateCategoryRequest Request) : IRequest<CategoryResponse>;
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
            var Category = await _db.Categories.SingleAsync(forDeletetion => forDeletetion.Id == command.id);
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
}
