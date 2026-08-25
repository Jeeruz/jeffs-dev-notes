using BlazorStrap;
using JeffsDevNotes.Client.Interfaces;
using JeffsDevNotes.Client.Services;
using JeffsDevNotes.Components;
using JeffsDevNotes.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

// ===================================================================================
// APPLICATION BUILDER INITIALIZATION
// ===================================================================================
// Create the web application builder with default configuration, command-line arguments,
// and environment settings (appsettings.json, environment variables, etc.).
var builder = WebApplication.CreateBuilder(args);

// Retrieve the PostgreSQL connection string from application configuration settings or environment variables.
var connectionString = builder.Configuration["SUPABASE_CONNECTION_STRING"];

// ===================================================================================
// 1. SERVICE REGISTRATION PHASE (Dependency Injection)
// ===================================================================================

// Register Razor Components services with support for Blazor WebAssembly interactive render mode.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Register the Entity Framework Core DbContext using the Npgsql provider for PostgreSQL integration.
builder.Services.AddDbContext<NotesContext>(options =>
    options.UseNpgsql(connectionString));

// Register BlazorStrap services to provide Bootstrap 5 UI component support in Blazor.
builder.Services.AddBlazorStrap();

// Register MVC Controller infrastructure to enable RESTful Web API endpoints (/api/[controller]).
builder.Services.AddControllers();

// Register Client Managers
builder.Services.AddScoped<INoteManager, NoteManager>();

// Register MediatR request handlers, notifications, and behaviors by scanning the server assembly.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Register a scoped HttpClient instance that dynamically resolves the base URI via NavigationManager.
// This ensures relative API calls succeed during Server-Side Rendering (SSR) and prerendering.
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});

// ===================================================================================
// APPLICATION HOST BUILD
// ===================================================================================
// Build the WebApplication host instance, locking in all registered services.
var app = builder.Build();

// ===================================================================================
// 2. MIDDLEWARE PIPELINE CONFIGURATION (HTTP Request Flow)
// ===================================================================================

// Configure environment-specific exception handling and security features.
if (app.Environment.IsDevelopment())
{
    // Enable browser debugging tools specifically for Blazor WebAssembly code.
    app.UseWebAssemblyDebugging();
}
else
{
    // Redirect unhandled exceptions to the custom error route in production.
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // Enforce HTTP Strict Transport Security (HSTS) in production environments.
    app.UseHsts();
}

// Re-execute requests returning non-success HTTP status codes (e.g., 404) to a clean custom route.
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

// Automatically redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enable endpoint routing to map incoming HTTP requests to controllers and Razor components.
app.UseRouting();

// Enable authorization middleware to enforce role-based and policy-based access control.
app.UseAuthorization();

// Enable anti-forgery protection (CSRF) for stateful form posts and interactivity.
app.UseAntiforgery();

// Map optimized static web assets (CSS, JavaScript, images, and WebAssembly artifacts).
app.MapStaticAssets();

// Map attribute-routed API controllers to expose Web API endpoints.
app.MapControllers();

// Map the root Blazor component (<App />) and enable interactive WebAssembly rendering mode.
// Also loads external assemblies containing client-side components and BlazorStrap UI assets.
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(JeffsDevNotes.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(BlazorStrap._Imports).Assembly);

// ===================================================================================
// APPLICATION RUN
// ===================================================================================
// Start listening for incoming HTTP requests.
app.Run();