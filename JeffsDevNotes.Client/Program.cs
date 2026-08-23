using BlazorStrap;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register HttpClient for client-side API requests using the browser's base host address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register BlazorStrap UI component services for WebAssembly client execution
builder.Services.AddBlazorStrap();

await builder.Build().RunAsync();