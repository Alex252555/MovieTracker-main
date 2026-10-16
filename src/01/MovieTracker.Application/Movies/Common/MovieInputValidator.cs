using FluentValidation;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.Common;

public abstract class MovieInputValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : IMovieInput
{
    protected MovieInputValidator()
    {
        RuleFor(request => request.Title)
            .Must(title => !string.IsNullOrWhiteSpace(title))
            .WithMessage("A movie must have a title.");

        RuleFor(request => request.Description)
            .Must(description => !string.IsNullOrWhiteSpace(description))
            .WithMessage("A movie must have a description.");

        RuleFor(request => request.Genre)
            .Must(genre =>
                Enum.TryParse<MovieGenre>(genre, ignoreCase: true, out var parsedGenre) &&
                Enum.IsDefined(parsedGenre))
            .WithMessage("Genre must be a valid movie genre.");

        RuleFor(request => request.ReleaseYear)
            .GreaterThanOrEqualTo(1888)
            .WithMessage("A movie's release year cannot be earlier than 1888.");

        RuleFor(request => request.PosterUrl)
            .Must(posterUrl => !string.IsNullOrWhiteSpace(posterUrl))
            .WithMessage("A movie must have a poster URL.");
    }
}
