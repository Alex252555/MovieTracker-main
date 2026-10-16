using MovieTracker.Application.Common.Repositories;
using MovieTracker.Application.Movies.Common;
using MovieTracker.Domain.Common.Results;

namespace MovieTracker.Application.Movies.UpdateMovie;

public sealed record UpdateMovieRequest(
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl) : IMovieInput;

public sealed class UpdateMovieHandler(IMovieRepository movieRepository)
{
    public async Task<Result> HandleAsync(
        Guid id,
        UpdateMovieRequest request,
        CancellationToken cancellationToken)
    {
        var movieResult = request.ToMovie(id);
        if (movieResult.IsFailure)
        {
            return Result.Failure(movieResult.Error);
        }

        return await movieRepository.UpdateAsync(movieResult.Value!, cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.NotFound);
    }
}
