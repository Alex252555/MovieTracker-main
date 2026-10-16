using System.Net.Http.Json;
using MovieTracker.Web.Models.Movies;

namespace MovieTracker.Web.Services.Movies;

public sealed class MovieApiClient(HttpClient httpClient)
{
    public async Task<MovieDetailsModel?> GetMovieByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync($"api/movies/{id}", cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MovieDetailsModel>(cancellationToken)
            ?? throw new InvalidOperationException("The movie API returned an empty response.");
    }

    public async Task<MovieListResponseModel> GetMoviesAsync(int page = 1, int pageSize = 24, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"api/movies?page={page}&pageSize={pageSize}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MovieListResponseModel>(cancellationToken)
            ?? throw new InvalidOperationException("The movie API returned an empty response.");
    }

    public async Task<Guid> CreateMovieAsync(
        MovieWriteRequestModel request,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync("api/movies", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        var createdMovie = await response.Content.ReadFromJsonAsync<CreateMovieResponseModel>(cancellationToken)
            ?? throw new InvalidOperationException("The movie API returned an empty response.");
        return createdMovie.Id;
    }

    public async Task UpdateMovieAsync(
        Guid id,
        MovieWriteRequestModel request,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PutAsJsonAsync($"api/movies/{id}", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteMovieAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.DeleteAsync($"api/movies/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    private sealed record CreateMovieResponseModel(Guid Id);
}
