using MovieTracker.Application.Movies.Common;
using MovieTracker.Domain.Common.Results;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.UpdateMovie;

internal static class UpdateMovieMapping
{
    internal static Result<Movie> ToMovie(this UpdateMovieRequest request, Guid id) =>
        MovieInputMapping.ToMovie(id, request);
}
