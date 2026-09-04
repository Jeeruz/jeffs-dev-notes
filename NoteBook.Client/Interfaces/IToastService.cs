using BlazorStrap;

namespace NoteBook.Client.Interfaces
{
    public interface IToastService
    {
        Task ShowAsync(BSColor Color, string title, string message);
    }
}
