using MovieTracker.Application.Movies.GetMovieById;
using MovieTracker.Domain.Movies;
using MovieTracker.UnitTests.Mocks;

namespace MovieTracker.UnitTests.Movies.GetMovieById;

[TestFixture]
public sealed class GetMovieByIdHandlerTests : BaseUnitTest
{
    [Test]
    public async Task HandleAsync_ReturnsMappedMovieWhenItExists()
    {
        var movie = Movie.Create(
            Guid.NewGuid(),
            Faker.Commerce.ProductName(),
            Faker.Lorem.Sentence(),
            MovieGenre.ScienceFiction,
            2016,
            "/images/posters/movie.svg").Value!;
        var handler = new GetMovieByIdHandler(new MockMovieRepository(1, [movie]));

        var response = await handler.HandleAsync(movie.Id, CancellationToken.None);

        Assert.That(response, Is.EqualTo(new GetMovieByIdResponse(
            movie.Id,
            movie.Title,
            movie.Description,
            "ScienceFiction",
            movie.ReleaseYear,
            movie.PosterUrl)));
    }

    [Test]
    public async Task HandleAsync_ReturnsNullWhenMovieDoesNotExist()
    {
        var handler = new GetMovieByIdHandler(new MockMovieRepository(0, []));

        var response = await handler.HandleAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.That(response, Is.Null);
    }
}
