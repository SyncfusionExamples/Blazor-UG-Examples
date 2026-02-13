using Gantt_GraphQl.Client.Pages;
using Gantt_GraphQl.Components;
using Gantt_GraphQl.Data;
using Gantt_GraphQl.GraphQl;
using Gantt_GraphQl.Model;

using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddSyncfusionBlazor();



// SQLite EF Core
builder.Services.AddDbContext<GanttDbContext>(options =>
    options.UseSqlite("Data Source=ganttdb.sqlite"));

builder.Services.AddScoped<GanttRepository>();

builder.Services
  .AddGraphQLServer()
  .AddQueryType<GanttQuery>()
  .AddMutationType<Mutation>()
  .AddType<TaskEntity>()
  .AddType<DataRequest>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GanttDbContext>();
    db.Database.EnsureCreated();
}

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

app.UseHttpsRedirection();


app.UseAntiforgery();
app.MapGraphQL("/graphql");

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Gantt_GraphQl.Client._Imports).Assembly);

app.Run();
