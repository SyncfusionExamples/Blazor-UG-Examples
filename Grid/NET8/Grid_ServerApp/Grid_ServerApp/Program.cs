using Syncfusion.Blazor;
using Grid_ServerApp.Components;
using static Grid_ServerApp.Components.Pages.CustomBinding.CAdapAllService;
using static Grid_ServerApp.Components.Pages.CustomBinding.CAdapService;
using static Grid_ServerApp.Components.Pages.CustomBinding.CustomADpWithDI;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<CustomAdaptor>();
builder.Services.AddScoped<ServiceClass>();
builder.Services.AddScoped<OrderServiceClass>();
builder.Services.AddScoped<OrderAllServiceClass>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
