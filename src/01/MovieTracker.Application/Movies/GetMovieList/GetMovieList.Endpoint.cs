using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MovieTracker.Application.Movies.GetMovieList;

public static class GetMovieListEndpoint
{
    public static IEndpointConventionBuilder MapGetMovieListEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/api/movies", HandleAsync)
            .WithName("GetMovieList")
            .WithSummary("Gets a paged list of movies")
            .Produces<GetMovieListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();

    private static async Task<IResult> HandleAsync(
        int page = 1,
        int pageSize = 12,
        IValidator<GetMovieListRequest> validator = null!,
        GetMovieListHandler handler = null!,
        CancellationToken cancellationToken = default)
    {
        var request = new GetMovieListRequest(page, pageSize);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);
        return Results.Ok(response);
    }
}
