using BlazorStrap;
using NoteBook.Client.Interfaces;

namespace NoteBook.Client.Services;

public class ToastService : IToastService
{
    private readonly IBlazorStrap _blazorStrap;

    public ToastService(IBlazorStrap blazorStrap)
    {
        _blazorStrap = blazorStrap;
    }

    public async Task ShowAsync(BSColor color, string title, string message)
    {
        _blazorStrap.Toaster.Add(title, message, options =>
        {
            options.Color = color;
            options.CloseAfter = 3000;
        });
    }
}