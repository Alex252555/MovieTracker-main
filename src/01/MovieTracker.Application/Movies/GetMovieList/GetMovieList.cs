using MovieTracker.Application.Common.Repositories;

namespace MovieTracker.Application.Movies.GetMovieList;

public sealed record GetMovieListRequest(int Page = 1, int PageSize = 12);

public sealed record MovieListItemResponse(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl);

public sealed record GetMovieListResponse(
    IReadOnlyList<MovieListItemResponse> Items,
    int Page,
    int PageSize,
    int TotalCount);

public sealed class GetMovieListHandler(IMovieRepository movieRepository)
{
    public async Task<GetMovieListResponse> HandleAsync(
        GetMovieListRequest request,
        CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * request.PageSize;
        var totalCount = await movieRepository.CountAsync(cancellationToken);
        var movies = await movieRepository.GetPagedAsync(skip, request.PageSize, cancellationToken);
        var items = movies.Select(movie => movie.ToResponse()).ToList();

        return new GetMovieListResponse(items, request.Page, request.PageSize, totalCount);
    }
}
