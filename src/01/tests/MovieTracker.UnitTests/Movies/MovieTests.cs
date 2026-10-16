using MovieTracker.Domain.Common.Results;
using MovieTracker.Domain.Movies;

namespace MovieTracker.UnitTests.Movies;

[TestFixture]
public sealed class MovieTests : BaseUnitTest
{
    [Test]
    public void Create_WithValidValues_ReturnsTheMovie()
    {
        var title = Faker.Commerce.ProductName();
        var description = Faker.Lorem.Sentence();
        var posterUrl = $"/images/posters/{Faker.Random.AlphaNumeric(10)}.svg";

        var result = Movie.Create(
            Guid.NewGuid(),
            $"  {title}  ",
            $"  {description}  ",
            MovieGenre.Drama,
            2024,
            $"  {posterUrl}  ");

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value!.Title, Is.EqualTo(title));
            Assert.That(result.Value.Description, Is.EqualTo(description));
            Assert.That(result.Value.PosterUrl, Is.EqualTo(posterUrl));
        });
    }

    [TestCase("id", "Movies.IdRequired", "A movie must have an id.")]
    [TestCase("title", "Movies.TitleRequired", "A movie must have a title.")]
    [TestCase("description", "Movies.DescriptionRequired", "A movie must have a description.")]
    [TestCase("releaseYear", "Movies.ReleaseYearTooEarly", "A movie's release year cannot be earlier than 1888.")]
    [TestCase("posterUrl", "Movies.PosterUrlRequired", "A movie must have a poster URL.")]
    public void Create_WithInvalidValue_ReturnsAnError(
        string invalidValue,
        string expectedErrorCode,
        string expectedErrorMessage)
    {
        var result = Movie.Create(
            invalidValue == "id" ? Guid.Empty : Guid.NewGuid(),
            invalidValue == "title" ? " " : Faker.Commerce.ProductName(),
            invalidValue == "description" ? " " : Faker.Lorem.Sentence(),
            MovieGenre.Drama,
            invalidValue == "releaseYear" ? 1887 : 2024,
            invalidValue == "posterUrl" ? " " : "/images/posters/movie.svg");

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Value, Is.Null);
            Assert.That(result.Error.Code, Is.EqualTo(expectedErrorCode));
            Assert.That(result.Error.Message, Is.EqualTo(expectedErrorMessage));
        });
    }
}
