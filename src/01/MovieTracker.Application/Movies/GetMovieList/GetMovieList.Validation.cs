using FluentValidation;

namespace MovieTracker.Application.Movies.GetMovieList;

public sealed class GetMovieListValidator : AbstractValidator<GetMovieListRequest>
{
    public GetMovieListValidator()
    {
        RuleFor(request => request.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be at least 1.");

        RuleFor(request => request.PageSize)
            .InclusiveBetween(1, 50)
            .WithMessage("Page size must be between 1 and 50.");
    }
}
