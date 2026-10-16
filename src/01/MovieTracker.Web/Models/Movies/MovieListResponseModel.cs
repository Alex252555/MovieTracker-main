namespace MovieTracker.Web.Models.Movies;

public sealed record MovieListResponseModel(IReadOnlyList<MovieListItemModel> Items, int Page, int PageSize, int TotalCount);
