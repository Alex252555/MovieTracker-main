using MovieTracker.Application.Common.Repositories;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.GetMovieById;

public sealed record GetMovieByIdResponse(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl);

public sealed class GetMovieByIdHandler(IMovieRepository movieRepository)
{
    public async Task<GetMovieByIdResponse?> HandleAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetByIdAsync(id, cancellationToken);
        return movie is null ? null : ToResponse(movie);
    }

    private static GetMovieByIdResponse ToResponse(Movie movie) =>
        new(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.Genre.ToString(),
            movie.ReleaseYear,
            movie.PosterUrl);
}
