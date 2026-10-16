namespace MovieTracker.Web.Models.Demo;

public sealed record DemoMovie(Guid Id, string Title, int Year, string Genre, int DurationMinutes, decimal Rating, string PosterPath, string Description);
public sealed record DemoRating(string Title, decimal Rating, DateOnly RatedOn);
public sealed record DemoReview(string Author, string Text, decimal Rating);
