using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using JeffsDevNotes.Shared.DTOs;

namespace JeffsDevNotes.Client.Pages
{
    public partial class Home : ComponentBase 
    {
        [Inject]
        public HttpClient Http { get; set; } = default!;

        // Page State
        protected List<NoteResponse> Notes { get; set; } = new();
        protected bool IsLoading { get; set; } = true;

        // Modal State and Model Binding 
        protected bool IsModalOpen { get; set; } = false;
        protected CreateNoteRequest CreateNoteModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadNotesAsync();
        }

        protected async Task LoadNotesAsync()
        {
            IsLoading = true;
            try
            {
                var result = await Http.GetFromJsonAsync<List<NoteResponse>>("api/notes");
                Notes = result ?? new List<NoteResponse>();
            }
            catch (Exception ex)
            {
                // Handle error (e.g., log it, show a message to the user)
                Console.Error.WriteLine($"Error loading notes: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Modal Visibility Control
        protected void OpenCreateModal()
        {
            CreateNoteModel = new CreateNoteRequest(); // Reset form model
            IsModalOpen = true;
        }
    }
}
