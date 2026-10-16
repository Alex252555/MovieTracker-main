using MovieTracker.Application.Movies.GetMovieList;

namespace MovieTracker.UnitTests.Movies.GetMovieList;

[TestFixture]
public sealed class GetMovieListValidatorTests
{
    private GetMovieListValidator validator = null!;

    [SetUp]
    public void SetUp() => validator = new GetMovieListValidator();

    [Test]
    public async Task ValidateAsync_DefaultRequest_IsValid()
    {
        var result = await validator.ValidateAsync(new GetMovieListRequest());

        Assert.That(result.IsValid, Is.True);
    }

    [TestCase(0, 12, nameof(GetMovieListRequest.Page))]
    [TestCase(1, 0, nameof(GetMovieListRequest.PageSize))]
    [TestCase(1, 51, nameof(GetMovieListRequest.PageSize))]
    public async Task ValidateAsync_InvalidPagination_HasExpectedError(
        int page,
        int pageSize,
        string propertyName)
    {
        var result = await validator.ValidateAsync(new GetMovieListRequest(page, pageSize));

        Assert.That(result.Errors.Select(error => error.PropertyName), Does.Contain(propertyName));
    }
}
