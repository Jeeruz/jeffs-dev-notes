using MediatR;
using Microsoft.EntityFrameworkCore;
using JeffsDevNotes.Data;
using JeffsDevNotes.Shared.DTOs;

namespace JeffsDevNotes.Application.Queries
{
    // Marks this record as a MediatR read request expecting a List<NoteResponse> return type
    public record GetNotesQuery : IRequest<List<NoteResponse>>;

    // Handles the read logic for GetNotesQuery
    public class  GetNotesQueryHandler : IRequestHandler<GetNotesQuery, List<NoteResponse>>
    {
        private readonly NotesContext _db; 
        public GetNotesQueryHandler(NotesContext db)
        {
            _db = db;
        }
        
        public async Task<List<NoteResponse>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            return await _db.Notes
                // Eager load related Category entity
                .Include(n => n.Category)
                // Optimization: Disables change tracking overhead for read-only queries
                .AsNoTracking()
                // Project database records directly into response DTOs using Updated properties
                .Select(n => new NoteResponse
                {
                    Id = n.Id,
                    Content = n.Content,
                    Description = n.Description,
                    CategoryId = n.CategoryId,
                    CategoryName = n.Category != null ? n.Category.Name : "Uncategorized"
                }).ToListAsync(cancellationToken);
        }
    }

}
