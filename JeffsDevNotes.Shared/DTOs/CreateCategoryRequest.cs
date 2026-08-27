namespace JeffsDevNotes.Shared.DTOs
{
    public class CreateCategoryRequest
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        //Constructor
        public CreateCategoryRequest (string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public CreateCategoryRequest(){}
    }
}

