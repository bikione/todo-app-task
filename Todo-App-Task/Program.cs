using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Todo_App_Task.App.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJsonToDoRepository>(provider => 
    new JsonToDoRepository("ToDoData.json"));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TODO LIST API V1");
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();