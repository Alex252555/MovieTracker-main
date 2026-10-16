using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MovieTracker.Domain.Common.Results;

namespace MovieTracker.Application.Movies.UpdateMovie;

public static class UpdateMovieEndpoint
{
    public static IEndpointConventionBuilder MapUpdateMovieEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapPut("/api/movies/{id:guid}", HandleAsync)
            .WithName("UpdateMovie")
            .WithSummary("Updates a movie")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();

    private static async Task<IResult> HandleAsync(
        Guid id,
        UpdateMovieRequest request,
        IValidator<UpdateMovieRequest> validator,
        UpdateMovieHandler handler,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var result = await handler.HandleAsync(id, request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error == Error.NotFound
                ? Results.NotFound()
                : Results.Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Error.Message);
        }

        return Results.NoContent();
    }
}
