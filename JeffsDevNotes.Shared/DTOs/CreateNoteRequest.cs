namespace JeffsDevNotes.Shared.DTOs
{
    public class CreateNoteRequest
    {
        public int Id { get; set; } = 0;
        public string Content { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }   
    }
}
