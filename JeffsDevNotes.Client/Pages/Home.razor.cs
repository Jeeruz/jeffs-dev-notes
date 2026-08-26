using Microsoft.AspNetCore.Components;
using JeffsDevNotes.Shared.DTOs;
using JeffsDevNotes.Client.Interfaces;
using JeffsDevNotes.Client.Components;

namespace JeffsDevNotes.Client.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; } = default!;

        [Inject]
        protected INoteManager NoteManager { get; set; } = default!;

        // Page State
        protected List<NoteResponse> Notes { get; set; } = new();
        protected List<CategoryResponse> Categories { get; set; } = new();
        protected bool IsLoading { get; set; } = true;

        // Modal State and Model Binding 
        protected CreateCategoryModal CreateCategoryModal { get; set; } = default!;
        protected CreateNoteModal CreateNoteModal { get; set; } = default!;
        protected bool IsNoteModalOpen { get; set; } = false;
        protected bool IsCategoryModalOpen { get; set; } = false;
        protected CreateNoteRequest CreateNoteModel { get; set; } = new();
        protected CreateCategoryRequest CreateCategoryModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Notes = await NoteManager.GetNotesAsync();
            Categories = await NoteManager.GetCategoriesAsync();
        }

        // Modal Visibility Control
        protected async Task OpenCreateNoteModal()
        {
            CreateNoteModel = new CreateNoteRequest(); 
            IsNoteModalOpen = true;

            await CreateNoteModal.ShowAsync();  
        }

        protected async Task OpenCreateCategoryModal()
        {
            CreateCategoryModel = new CreateCategoryRequest();
            IsCategoryModalOpen = true;

            await CreateCategoryModal.ShowAsync();
        }
        
        protected async Task CreateNote(CreateNoteRequest createNoteModel)
        {
            var response = await NoteManager.CreateNoteAsync(createNoteModel);

            if (response != null && response.Id != 0)
            {
                Notes.Add(response);
            }
        }

        protected async Task CreateCategory(CreateCategoryRequest createCategoryModel)
        {
            var response = await NoteManager.CreateCategoryAsync(createCategoryModel);

            if (response != null && response.Id != 0)
            {
                Categories.Add(response);
            }
        }
    }
}



