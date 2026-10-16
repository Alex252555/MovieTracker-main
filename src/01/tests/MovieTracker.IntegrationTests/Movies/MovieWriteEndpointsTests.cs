using System.Net;
using System.Net.Http.Json;
using MovieTracker.Application.Movies.CreateMovie;
using MovieTracker.Application.Movies.GetMovieById;
using MovieTracker.Application.Movies.UpdateMovie;

namespace MovieTracker.IntegrationTests.Movies;

[TestFixture]
public sealed class MovieWriteEndpointsTests : BaseIntegrationTest
{
    [Test]
    public async Task CreateUpdateAndDeleteMovie_ChangesTheMovieCatalogue()
    {
        var createRequest = new CreateMovieRequest(
            "Test feature film",
            "A test description.",
            "Drama",
            2024,
            "/images/posters/test-feature-film.svg");

        var createResponse = await Client.PostAsJsonAsync("/api/movies", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<CreateMovieResponse>();

        Assert.Multiple(() =>
        {
            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(created, Is.Not.Null);
        });

        var id = created!.Id;
        var initialDetails = await Client.GetFromJsonAsync<GetMovieByIdResponse>($"/api/movies/{id}");
        Assert.That(initialDetails!.Title, Is.EqualTo(createRequest.Title));

        var updateRequest = new UpdateMovieRequest(
            "Updated feature film",
            "An updated test description.",
            "ScienceFiction",
            2025,
            "/images/posters/updated-feature-film.svg");
        var updateResponse = await Client.PutAsJsonAsync($"/api/movies/{id}", updateRequest);
        var updatedDetails = await Client.GetFromJsonAsync<GetMovieByIdResponse>($"/api/movies/{id}");

        Assert.Multiple(() =>
        {
            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(updatedDetails!.Title, Is.EqualTo(updateRequest.Title));
            Assert.That(updatedDetails.Genre, Is.EqualTo(updateRequest.Genre));
            Assert.That(updatedDetails.ReleaseYear, Is.EqualTo(updateRequest.ReleaseYear));
        });

        var deleteResponse = await Client.DeleteAsync($"/api/movies/{id}");
        var getDeletedResponse = await Client.GetAsync($"/api/movies/{id}");

        Assert.Multiple(() =>
        {
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(getDeletedResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        });
    }

    [Test]
    public async Task CreateMovie_WithInvalidInput_ReturnsValidationProblem()
    {
        var request = new CreateMovieRequest(
            " ",
            "",
            "UnknownGenre",
            1887,
            " ");

        var response = await Client.PostAsJsonAsync("/api/movies", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task UpdateAndDeleteMovie_WhenMovieDoesNotExist_ReturnNotFound()
    {
        var id = Guid.NewGuid();
        var updateRequest = new UpdateMovieRequest(
            "Title",
            "Description",
            "Drama",
            2024,
            "/images/posters/test.svg");

        var updateResponse = await Client.PutAsJsonAsync($"/api/movies/{id}", updateRequest);
        var deleteResponse = await Client.DeleteAsync($"/api/movies/{id}");

        Assert.Multiple(() =>
        {
            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        });
    }
}
