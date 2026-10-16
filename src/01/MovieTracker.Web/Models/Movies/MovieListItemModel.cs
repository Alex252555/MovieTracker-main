namespace MovieTracker.Web.Models.Movies;

public sealed record MovieListItemModel(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl);
