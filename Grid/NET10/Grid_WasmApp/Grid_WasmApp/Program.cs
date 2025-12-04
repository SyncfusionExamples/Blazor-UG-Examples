using Grid_WasmApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using static Grid_WasmApp.Pages.CustomBinding.CAdapAllService;
using static Grid_WasmApp.Pages.CustomBinding.CAdapService;
using static Grid_WasmApp.Pages.CustomBinding.CustomADpWithDI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<CustomAdaptor>();
builder.Services.AddScoped<ServiceClass>();
builder.Services.AddScoped<OrderServiceClass>();
builder.Services.AddScoped<OrderAllServiceClass>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
