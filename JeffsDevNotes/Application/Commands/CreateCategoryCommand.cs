using MediatR;
using JeffsDevNotes.Data;
using JeffsDevNotes.Shared;
using JeffsDevNotes.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JeffsDevCategorys.Application.Commands
{
    // Marks this record as a MediatR write request expecting a CategoryResponse return type
    public record CreateCategoryCommand(CreateCategoryRequest Request) : IRequest<CategoryResponse>;
    public record UpdateCategoryCommand(int id, CreateCategoryRequest Request) : IRequest<CategoryResponse>;
    public record DeleteCategoryCommand(int id) : IRequest<CategoryResponse>;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public CreateCategoryCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
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
            return new CategoryResponse
            {
                Id = Category.Id,
                Name = Category.Name,
                Description = Category.Description,
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

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryResponse>
    {
        private readonly NotesContext _db;

        // Injects the Entity Framework database context
        public DeleteCategoryCommandHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<CategoryResponse> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            // Map the incoming payload to the domain entity
            var Category = await _db.Categories.SingleAsync(forDeletetion => forDeletetion.Id == command.id);

            // Stage and persist the new record to the database asynchronously
            _db.Categories.Remove(Category);
            var status = await _db.SaveChangesAsync(cancellationToken);

            // Return flattened response DTO
            return new CategoryResponse();
        }
    }
}
