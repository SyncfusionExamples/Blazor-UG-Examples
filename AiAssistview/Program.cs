using AiAssistview.Components;
using Syncfusion.Blazor;
using AIAssistview.Components.Services;
using Microsoft.Extensions.AI;
using OllamaSharp;
using Azure.AI.OpenAI;
using Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<AzureOpenAIService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();

    var endpoint = "https://azure-testresource.openai.azure.com";
    var apiKey = "<Your API Key>"; // Replace with your API key;
    var deploymentName = "gpt-4o-mini";

    return new AzureOpenAIService(httpClient, endpoint, apiKey, deploymentName);
});

builder.Services.AddDistributedMemoryCache();

// Ollama configuration
builder.Services.AddChatClient(new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2"))
    .UseDistributedCache()
    .UseLogging();

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
