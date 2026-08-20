using Microsoft.EntityFrameworkCore;
using JeffsDevNotes.Shared;

namespace JeffsDevNotes.Data
{
    public class NotesContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Constructor passes DI options (connection string, provider) to base DbContext
        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {
        }
    }
}
