using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Data;
using WebAPIProject.Models;
using WebAPIProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //Connects to the database using the connection string from appsettings.json

builder.Services.AddValidatorsFromAssemblyContaining<CreateTodoRequestValidator>(); //Registers all validators in assembly or "file"

builder.Services.AddFluentValidationAutoValidation(); //Enables automatic validation for incoming requests

builder.Services.AddControllers(); //Starts alla controllers in the project

builder.Services.AddScoped<TodoService>(); //Registers service for dependyinjection

builder.Services.AddEndpointsApiExplorer(); //Not app.MapOpenApi, had issues check "documentation.txt" @ *1 for more information

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
