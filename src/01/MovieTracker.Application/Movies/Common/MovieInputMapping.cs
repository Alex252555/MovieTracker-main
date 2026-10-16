using MovieTracker.Domain.Common.Results;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.Common;

internal static class MovieInputMapping
{
    internal static Result<Movie> ToMovie(
        Guid id,
        IMovieInput request)
    {
        if (!Enum.TryParse<MovieGenre>(request.Genre, ignoreCase: true, out var genre) ||
            !Enum.IsDefined(genre))
        {
            return MovieErrors.GenreInvalid;
        }

        return Movie.Create(
            id,
            request.Title,
            request.Description,
            genre,
            request.ReleaseYear,
            request.PosterUrl);
    }
}
