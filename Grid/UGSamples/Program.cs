using Syncfusion.Blazor;
using UGSamples.Components;
using static UGSamples.Components.Pages.CustomBinding.CAdapAllService;
using static UGSamples.Components.Pages.CustomBinding.CAdapService;
using static UGSamples.Components.Pages.CustomBinding.CustomADpWithDI;

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


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
