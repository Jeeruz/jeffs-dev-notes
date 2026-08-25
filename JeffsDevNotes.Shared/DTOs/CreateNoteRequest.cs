namespace JeffsDevNotes.Shared.DTOs
{
    public class CreateNoteRequest
    {
        public string Content { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }   
    }
}
