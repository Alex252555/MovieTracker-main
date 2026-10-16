using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MovieTracker.Domain.Common.Results;

namespace MovieTracker.Application.Movies.CreateMovie;

public static class CreateMovieEndpoint
{
    public static IEndpointConventionBuilder MapCreateMovieEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapPost("/api/movies", HandleAsync)
            .WithName("CreateMovie")
            .WithSummary("Creates a movie")
            .Produces<CreateMovieResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict);

    private static async Task<IResult> HandleAsync(
        CreateMovieRequest request,
        IValidator<CreateMovieRequest> validator,
        CreateMovieHandler handler,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var result = await handler.HandleAsync(request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error == Error.Exist
                ? Results.Problem(statusCode: StatusCodes.Status409Conflict, title: result.Error.Message)
                : Results.Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Error.Message);
        }

        var response = result.Value!;
        return Results.Created($"/api/movies/{response.Id}", response);
    }
}
