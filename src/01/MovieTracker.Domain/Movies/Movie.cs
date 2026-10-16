using MovieTracker.Domain.Common.Results;

namespace MovieTracker.Domain.Movies;

public sealed class Movie
{
    private Movie(
        Guid id,
        string title,
        string description,
        MovieGenre genre,
        int releaseYear,
        string posterUrl)
    {
        Id = id;
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
        Genre = genre;
        PosterUrl = posterUrl;
    }

    public static Result<Movie> Create(
        Guid id,
        string title,
        string description,
        MovieGenre genre,
        int releaseYear,
        string posterUrl)
    {
        if (id == Guid.Empty)
        {
            return MovieErrors.IdRequired;
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return MovieErrors.TitleRequired;
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return MovieErrors.DescriptionRequired;
        }

        if (releaseYear < 1888)
        {
            return MovieErrors.ReleaseYearTooEarly;
        }

        if (string.IsNullOrWhiteSpace(posterUrl))
        {
            return MovieErrors.PosterUrlRequired;
        }

        var movie = new Movie(
            id,
            title.Trim(),
            description.Trim(),
            genre,
            releaseYear,
            posterUrl.Trim());

        return Result<Movie>.Success(movie);
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int ReleaseYear { get; private set; }
    public MovieGenre Genre { get; private set; }
    public string PosterUrl { get; private set; } = string.Empty;
}
