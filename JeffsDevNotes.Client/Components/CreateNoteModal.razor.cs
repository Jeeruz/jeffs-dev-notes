using BlazorStrap.V5;
using JeffsDevNotes.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace JeffsDevNotes.Client.Components
{
    public partial class CreateNoteModal
    {
        private BSModal modalRef = default!;

        [Parameter]
        public EventCallback<CreateNoteRequest> OnNoteCreated { get; set; }

        protected CreateNoteRequest CreateNoteModel { get; set; } = new();

        // Expose a method for parent pages to call directly via @ref
        public async Task ShowAsync()
        {
            CreateNoteModel = new CreateNoteRequest(); // Reset form model
            await modalRef.ShowAsync();
        }

        public async Task HideAsync()
        {
            await modalRef.HideAsync();
        }

        private async Task SaveAsync()
        {
            if (!string.IsNullOrWhiteSpace(CreateNoteModel.Content))
            {
                await OnNoteCreated.InvokeAsync(CreateNoteModel);
                await HideAsync();
            }
        }
    }
}
