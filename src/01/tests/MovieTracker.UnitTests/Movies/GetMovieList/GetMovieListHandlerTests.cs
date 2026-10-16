using MovieTracker.Application.Movies.GetMovieList;
using MovieTracker.Domain.Movies;
using MovieTracker.UnitTests.Mocks;

namespace MovieTracker.UnitTests.Movies.GetMovieList;

[TestFixture]
public sealed class GetMovieListHandlerTests : BaseUnitTest
{
    [Test]
    public async Task HandleAsync_ReturnsTheRequestedMappedPage()
    {
        var creationResult = Movie.Create(
            Guid.NewGuid(),
            Faker.Commerce.ProductName(),
            Faker.Lorem.Sentence(),
            MovieGenre.ScienceFiction,
            2016,
            $"/images/posters/{Faker.Random.AlphaNumeric(10)}.svg");
        var movie = creationResult.Value!;
        var repository = new MockMovieRepository(totalCount: 8, [movie]);
        var handler = new GetMovieListHandler(repository);

        var response = await handler.HandleAsync(
            new GetMovieListRequest(Page: 2, PageSize: 3),
            CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(creationResult.IsSuccess, Is.True);
            Assert.That(repository.RequestedSkip, Is.EqualTo(3));
            Assert.That(repository.RequestedTake, Is.EqualTo(3));
            Assert.That(response.Page, Is.EqualTo(2));
            Assert.That(response.PageSize, Is.EqualTo(3));
            Assert.That(response.TotalCount, Is.EqualTo(8));
            Assert.That(response.Items, Has.Count.EqualTo(1));
            Assert.That(response.Items[0], Is.EqualTo(new MovieListItemResponse(
                movie.Id,
                movie.Title,
                movie.Description,
                "ScienceFiction",
                movie.ReleaseYear,
                movie.PosterUrl)));
        });
    }
}
