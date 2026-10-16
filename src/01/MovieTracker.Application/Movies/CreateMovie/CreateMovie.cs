using MovieTracker.Application.Common.Repositories;
using MovieTracker.Application.Movies.Common;
using MovieTracker.Domain.Common.Results;

namespace MovieTracker.Application.Movies.CreateMovie;

public sealed record CreateMovieRequest(
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl) : IMovieInput;

public sealed record CreateMovieResponse(Guid Id);

public sealed class CreateMovieHandler(IMovieRepository movieRepository)
{
    public async Task<Result<CreateMovieResponse>> HandleAsync(
        CreateMovieRequest request,
        CancellationToken cancellationToken)
    {
        var movieResult = request.ToMovie(Guid.NewGuid());
        if (movieResult.IsFailure)
        {
            return movieResult.Error;
        }

        var movie = movieResult.Value!;
        if (!await movieRepository.CreateAsync(movie, cancellationToken))
        {
            return Error.Exist;
        }

        return Result<CreateMovieResponse>.Success(new CreateMovieResponse(movie.Id));
    }
}
