using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

//Register Syncfusion license
//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSyncfusionBlazor();	

await builder.Build().RunAsync();
