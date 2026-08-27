using JeffsDevNotes.Client.Components;
using JeffsDevNotes.Client.Interfaces;
using JeffsDevNotes.Client.Services;
using JeffsDevNotes.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace JeffsDevNotes.Client.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; } = default!;
        [Inject]
        protected INote NoteService { get; set; } = default!;
        [Inject]
        protected ICategory CategoryService { get; set; } = default!;

        // Page State
        protected List<NoteResponse> Notes { get; set; } = new();
        protected List<CategoryResponse> Categories { get; set; } = new();
        protected bool IsLoading { get; set; } = true;

        // Modal State and Model Binding 
        protected NoteModal NoteModal { get; set; } = default!;
        protected bool IsNoteModalOpen { get; set; } = false;
        protected bool IsnoteModalOpen { get; set; } = false;
        protected CreateNoteRequest NoteModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Notes = await NoteService.GetNotesAsync();
            Categories = await CategoryService.GetCategoriesAsync();
        }

        // Modal Visibility Control
        protected async Task OpenCreateNoteModal()
        {
            NoteModel = new CreateNoteRequest(); 
            await NoteModal.ShowAsync();  
        }
        
        protected async Task CreateNote(CreateNoteRequest createNoteModel)
        {
            var response = await NoteService.CreateNoteAsync(createNoteModel);

            if (response != null && response.Id != 0)
            {
                Notes.Add(response);
            }
        }

        protected async Task UpdateNote(CreateNoteRequest createNoteModel)
        {
            var response = await NoteService.UpdateNoteAsync(createNoteModel.Id, createNoteModel);

            if (response != null && response.Id != 0)
            {
             
            }
        }

        protected async Task DeleteNote(CreateNoteRequest createNoteModel)
        {
            var response = await NoteService.DeleteNoteAsync(createNoteModel.Id);

            if (response != false)
            {
                Notes = Notes.Where(note => note.Id != createNoteModel.Id).ToList();
            }
        }
    }
}



