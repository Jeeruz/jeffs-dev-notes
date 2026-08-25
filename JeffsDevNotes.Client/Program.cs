using BlazorStrap;
using JeffsDevNotes.Client.Interfaces;
using JeffsDevNotes.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// ===================================================================================
// BROWSER CLIENT BUILDER INITIALIZATION
// ===================================================================================
// Create the WebAssembly host builder to initialize the browser runtime environment,
// configuration providers, and WebAssembly-specific services.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// ===================================================================================
// SERVICE REGISTRATION PHASE (Client-Side Dependency Injection)
// ===================================================================================

// Register a scoped HttpClient instance configured with the browser's base domain address.
// This allows client components to make relative HTTP requests (e.g., API calls) back to the server host.
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register BlazorStrap services to enable Bootstrap 5 UI component rendering on the client side.
builder.Services.AddBlazorStrap();

// Register Client Managers
builder.Services.AddScoped<INoteManager, NoteManager>();

// ===================================================================================
// APPLICATION HOST BUILD & RUN
// ===================================================================================
// Build the WebAssembly client host and start asynchronous execution inside the user's browser runtime.
await builder.Build().RunAsync();