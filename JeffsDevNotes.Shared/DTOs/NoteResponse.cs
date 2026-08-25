namespace JeffsDevNotes.Shared.DTOs
{
    public class NoteResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
