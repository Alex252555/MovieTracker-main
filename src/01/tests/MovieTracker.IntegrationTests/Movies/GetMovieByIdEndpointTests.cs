using System.Net;
using System.Net.Http.Json;
using MovieTracker.Application.Movies.GetMovieById;

namespace MovieTracker.IntegrationTests.Movies;

[TestFixture]
public sealed class GetMovieByIdEndpointTests : BaseIntegrationTest
{
    [Test]
    public async Task GetMovieById_ReturnsMovieFromTheInMemoryCatalogue()
    {
        var id = Guid.Parse("00000000-0000-0000-0000-000000000001");

        var response = await Client.GetAsync($"/api/movies/{id}");
        var movie = await response.Content.ReadFromJsonAsync<GetMovieByIdResponse>();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(movie, Is.Not.Null);
            Assert.That(movie!.Id, Is.EqualTo(id));
            Assert.That(movie.Title, Is.EqualTo("Interstellar"));
            Assert.That(movie.Genre, Is.EqualTo("ScienceFiction"));
        });
    }

    [Test]
    public async Task GetMovieById_ReturnsNotFoundWhenMovieDoesNotExist()
    {
        var response = await Client.GetAsync($"/api/movies/{Guid.NewGuid()}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
