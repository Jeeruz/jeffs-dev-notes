using NoteBook.Client.Components;
using NoteBook.Client.Interfaces;
using NoteBook.Client.Services;
using NoteBook.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace NoteBook.Client.Pages
{
    public partial class Note : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; } = default!;
        [Inject]
        protected INoteService NoteService { get; set; } = default!;
        [Inject]
        protected ICategory CategoryService { get; set; } = default!;

        // Page State
        protected List<NoteResponse> Notes { get; set; } = new();
        protected List<CategoryResponse> Categories { get; set; } = new();
        protected bool IsLoading { get; set; } = true;

        // Modal State and Model Binding 
        protected NoteModal NoteModalCreate { get; set; } = default!;
        protected NoteModal NoteModalUpdate { get; set; } = default!;
        protected NoteModal NoteModalDelete { get; set; } = default!;
        protected CreateNoteRequest NoteModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Notes = await NoteService.GetNotesAsync();
            Categories = await CategoryService.GetCategoriesAsync();
        }

        // Modal Visibility Control
        protected async Task OpenNoteModal(string action, int id)
        {        
            await SelectRow(id);
            switch (action)
            {
                case "create":
                    NoteModel = new CreateNoteRequest();
                    await NoteModalCreate.ShowAsync();
                    break;
                case "update":
                    await NoteModalUpdate.ShowAsync();
                    break;
                case "delete":
                    await NoteModalDelete.ShowAsync();
                    break;
                default:
                    break;
            }       
        }

        private async Task SelectRow(int id)
        {
            var match = Notes.FirstOrDefault(note => note.Id == id);
            if (match != null)
            {
                NoteModel = new CreateNoteRequest
                {
                    Id = match.Id,
                    Content = match.Content,
                    Description = match.Description,
                    CategoryId = match.CategoryId,
                };
            }
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
                if (response != null && response.Id != 0)
                {
                    // Search the in-memory list for the position (0-based index) of the existing item matching the updated ID.
                    // Returns -1 if no matching category is found.
                    var index = Notes.FindIndex(c => c.Id == response.Id);

                    // Check if the item actually exists in the list
                    if (index != -1)
                    {
                        // Replace the old object directly at its existing position with the updated response.
                        // This updates the specific row in-place, preserving list order and triggering a clean Blazor UI re-render.
                        Notes[index] = response;
                    }
                }
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



