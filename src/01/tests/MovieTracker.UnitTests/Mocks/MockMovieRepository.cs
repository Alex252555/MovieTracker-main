using MovieTracker.Application.Common.Repositories;
using MovieTracker.Domain.Movies;

namespace MovieTracker.UnitTests.Mocks;

public sealed class MockMovieRepository(
    int totalCount,
    IReadOnlyList<Movie> movies) : IMovieRepository
{
    public int RequestedSkip { get; private set; }
    public int RequestedTake { get; private set; }

    public Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        Task.FromResult(movies.FirstOrDefault(movie => movie.Id == id));

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(totalCount);

    public Task<IReadOnlyList<Movie>> GetPagedAsync(
        int skip,
        int take,
        CancellationToken cancellationToken = default)
    {
        RequestedSkip = skip;
        RequestedTake = take;
        return Task.FromResult(movies);
    }

    public Task<bool> CreateAsync(Movie movie, CancellationToken cancellationToken = default) =>
        Task.FromResult(false);

    public Task<bool> UpdateAsync(Movie movie, CancellationToken cancellationToken = default) =>
        Task.FromResult(false);

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        Task.FromResult(false);
}
