using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Common.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Movie>> GetPagedAsync(
        int skip,
        int take,
        CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(Movie movie, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Movie movie, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
