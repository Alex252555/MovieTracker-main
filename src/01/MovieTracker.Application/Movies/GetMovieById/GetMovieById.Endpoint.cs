using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MovieTracker.Application.Movies.GetMovieById;

public static class GetMovieByIdEndpoint
{
    public static IEndpointConventionBuilder MapGetMovieByIdEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/api/movies/{id:guid}", HandleAsync)
            .WithName("GetMovieById")
            .WithSummary("Gets a movie by its id")
            .Produces<GetMovieByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

    private static async Task<IResult> HandleAsync(
        Guid id,
        GetMovieByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var movie = await handler.HandleAsync(id, cancellationToken);
        return movie is null ? Results.NotFound() : Results.Ok(movie);
    }
}
