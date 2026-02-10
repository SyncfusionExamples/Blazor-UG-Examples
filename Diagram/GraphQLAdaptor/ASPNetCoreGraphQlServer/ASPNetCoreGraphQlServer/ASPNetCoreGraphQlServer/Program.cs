using ASPNetCoreGraphQlServer.GraphQl;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer().AddQueryType<GraphQLQuery>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7021")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials().Build();
    });
});


var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapGraphQL());
app.MapGet("/", () => "Hello World!");
app.Run();
