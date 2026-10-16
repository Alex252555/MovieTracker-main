using MovieTracker.Application.Movies.Common;
using MovieTracker.Domain.Common.Results;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.CreateMovie;

internal static class CreateMovieMapping
{
    internal static Result<Movie> ToMovie(this CreateMovieRequest request, Guid id) =>
        MovieInputMapping.ToMovie(id, request);
}
