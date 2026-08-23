using BlazorStrap;
using JeffsDevNotes.Components;
using JeffsDevNotes.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Retrieve PostgreSQL connection string from application settings or environment variables
var connectionString = builder.Configuration["SUPABASE_CONNECTION_STRING"];

// Add Blazor UI component services and enable WebAssembly interactive rendering mode
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Register EF Core DbContext with PostgreSQL driver (Npgsql)
builder.Services.AddDbContext<NotesContext>(options =>
    options.UseNpgsql(connectionString));

// Register BlazorStrap UI services for Bootstrap components
builder.Services.AddBlazorStrap();

// Register Web API Controller routing infrastructure
builder.Services.AddControllers();

// Register MediatR handlers by scanning the Server assembly
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Register HttpClient using NavigationManager to automatically detect the base URL during SSR/prerendering
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});

var app = builder.Build();

// Configure environment-specific error handling middleware
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Enable standard ASP.NET Core middleware pipeline
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();

// Map static web assets (CSS, JS, images)
app.MapStaticAssets();

// Map Web API Controllers to support /api/[controller] endpoints
app.MapControllers();

// Map root Blazor application component and register WebAssembly assemblies
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(JeffsDevNotes.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(BlazorStrap._Imports).Assembly);

app.Run();