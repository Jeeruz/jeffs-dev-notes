using System;
using System.Collections.Generic;
using System.Text;

namespace JeffsDevNotes.Shared
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Foreign Key Column
        public int CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }
    }

}
