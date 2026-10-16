namespace MovieTracker.Application.Movies.Common;

public interface IMovieInput
{
    string Title { get; }
    string Description { get; }
    string Genre { get; }
    int ReleaseYear { get; }
    string PosterUrl { get; }
}
