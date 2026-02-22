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

builder.Services.AddValidatorsFromAssemblyContaining<CreateTournamentRequestValidator>(); //Registers all validators in assembly or "file"

builder.Services.AddFluentValidationAutoValidation(); //Enables automatic validation for incoming requests

builder.Services.AddControllers(); //Starts alla controllers in the project

builder.Services.AddScoped<TournamentsService>(); //Registers service for dependyinjection

builder.Services.AddScoped<GamesService>(); //Registers service for dependyinjection

builder.Services.AddEndpointsApiExplorer(); //Not app.MapOpenApi, had issues check "documentation.txt" @ *1 for more information
builder.Services.AddSwaggerGen();          //^^

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //"documentation.txt" @ *1
    app.UseSwaggerUI(); //"documentation.txt" @ *1
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
