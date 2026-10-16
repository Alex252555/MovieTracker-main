using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.GetMovieList;

internal static class GetMovieListMapping
{
    internal static MovieListItemResponse ToResponse(this Movie movie) =>
        new(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.Genre.ToString(),
            movie.ReleaseYear,
            movie.PosterUrl);
}
