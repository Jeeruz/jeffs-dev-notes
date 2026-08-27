using BlazorStrap.V5;
using JeffsDevNotes.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace JeffsDevNotes.Client.Components
{
    public partial class NoteModal
    {
        private BSModal modalRef = default!;

        [Parameter]
        public EventCallback<CreateNoteRequest> OnNoteEvent { get; set; }

        [Parameter]
        public CreateNoteRequest NoteRequest { get; set; } = new();

        [Parameter]
        public bool Delete { get; set; } = false;

        [Parameter]
        public List<CategoryResponse> Categories { get; set; } = new();

        // Expose a method for parent pages to call directly via @ref
        public async Task ShowAsync()
        {
            NoteRequest = NoteRequest.Id != 0 ? NoteRequest : new();
            await modalRef.ShowAsync();
        }

        public async Task HideAsync()
        {
            await modalRef.HideAsync();
        }

        private async Task SaveAsync()
        {
            if (!string.IsNullOrWhiteSpace(NoteRequest.Content))
            {
                await OnNoteEvent.InvokeAsync(NoteRequest);
                await HideAsync();
            } 
        }
    }
}
