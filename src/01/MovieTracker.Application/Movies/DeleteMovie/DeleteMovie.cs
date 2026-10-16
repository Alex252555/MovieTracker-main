using MovieTracker.Application.Common.Repositories;
using MovieTracker.Domain.Common.Results;

namespace MovieTracker.Application.Movies.DeleteMovie;

public sealed class DeleteMovieHandler(IMovieRepository movieRepository)
{
    public async Task<Result> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        return await movieRepository.DeleteAsync(id, cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.NotFound);
    }
}
