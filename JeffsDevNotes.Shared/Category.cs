using System;
using System.Collections.Generic;
using System.Text;

namespace JeffsDevNotes.Shared
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation Property (One category has many notes)
        public List<Note> Notes { get; set; } = new();
    }
}
