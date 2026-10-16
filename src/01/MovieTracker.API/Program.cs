using FluentValidation;
using MovieTracker.Application.Common.Repositories;
using MovieTracker.Application.Movies.CreateMovie;
using MovieTracker.Application.Movies.DeleteMovie;
using MovieTracker.Application.Movies.GetMovieById;
using MovieTracker.Application.Movies.GetMovieList;
using MovieTracker.Application.Movies.UpdateMovie;
using MovieTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMovieRepository, InMemoryMovieRepository>();
builder.Services.AddScoped<CreateMovieHandler>();
builder.Services.AddScoped<IValidator<CreateMovieRequest>, CreateMovieValidator>();
builder.Services.AddScoped<DeleteMovieHandler>();
builder.Services.AddScoped<GetMovieByIdHandler>();
builder.Services.AddScoped<GetMovieListHandler>();
builder.Services.AddScoped<IValidator<GetMovieListRequest>, GetMovieListValidator>();
builder.Services.AddScoped<UpdateMovieHandler>();
builder.Services.AddScoped<IValidator<UpdateMovieRequest>, UpdateMovieValidator>();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("MovieTrackerWeb", policy =>
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("MovieTrackerWeb");
app.MapCreateMovieEndpoint();
app.MapDeleteMovieEndpoint();
app.MapGetMovieByIdEndpoint();
app.MapGetMovieListEndpoint();
app.MapUpdateMovieEndpoint();

app.Run();

public partial class Program;
