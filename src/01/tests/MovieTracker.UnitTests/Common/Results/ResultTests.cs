using MovieTracker.Domain.Common.Results;

namespace MovieTracker.UnitTests.Common.Results;

[TestFixture]
public sealed class ResultTests
{
    [Test]
    public void Success_WithValue_CreatesSuccessfulResult()
    {
        var result = Result<string>.Success("movie");

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Value, Is.EqualTo("movie"));
            Assert.That(result.Error, Is.EqualTo(Error.None));
        });
    }

    [Test]
    public void Error_ImplicitlyConvertsToFailedResult()
    {
        var error = new Error("Movies.Invalid", "The movie is invalid.");

        Result<string> result = error;

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Value, Is.Null);
            Assert.That(result.Error, Is.EqualTo(error));
        });
    }
}
