using MediatR;
using Microsoft.EntityFrameworkCore;
using NoteBook.Data;
using NoteBook.Shared.DTOs;

namespace NoteBook.Application.Queries
{
    // Marks this record as a MediatR read request expecting a List<CategoryResponse> return type
    public record GetCategoryQuery : IRequest<List<CategoryResponse>>;

    // Handles the read logic for GetCategoryQuery
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<CategoryResponse>>
    {
        private readonly NotesContext _db;
        public GetCategoryQueryHandler(NotesContext db)
        {
            _db = db;
        }

        public async Task<List<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _db.Categories
                // Optimization: Disables change tracking overhead for read-only queries
                .AsNoTracking()
                // Project database records directly into response DTOs using Updated properties
                .Select(n => new CategoryResponse
                {
                    Id = n.Id,
                    Name = n.Name,
                    Description = n.Description
                }).ToListAsync(cancellationToken);
        }
    }

}       
