using JeffsDevNotes.Components;     
using JeffsDevNotes.Data;
using Microsoft.EntityFrameworkCore;
using BlazorStrap;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["SUPABASE_CONNECTION_STRING"];
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddDbContext<NotesContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddBlazorStrap();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(JeffsDevNotes.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(BlazorStrap._Imports).Assembly);

app.Run();
