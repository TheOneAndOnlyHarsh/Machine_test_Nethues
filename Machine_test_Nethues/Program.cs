using Machine_test_Nethues.Components;
using Machine_test_Nethues.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IAdditionService, AdditionService>();
builder.Services.AddScoped<ILogService, LogService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapGet("/logs/{*filePath}", async (string filePath) =>
{
    var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", filePath);
    if (System.IO.File.Exists(file))
    {
        var contentType = "application/octet-stream"; // You may want to change this based on your needs
        return Results.File(file, contentType, Path.GetFileName(file));
    }
    return Results.NotFound();
});
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
