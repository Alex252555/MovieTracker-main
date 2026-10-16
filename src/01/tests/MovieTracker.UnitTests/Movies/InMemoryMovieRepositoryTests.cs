using MovieTracker.Domain.Movies;
using MovieTracker.Infrastructure.Repositories;

namespace MovieTracker.UnitTests.Movies;

[TestFixture]
public sealed class InMemoryMovieRepositoryTests
{
    [Test]
    public async Task CreateAsync_AddsMovieThatCanBeReadById()
    {
        var repository = new InMemoryMovieRepository();
        var movie = CreateMovie();

        var created = await repository.CreateAsync(movie);
        var found = await repository.GetByIdAsync(movie.Id);

        Assert.Multiple(() =>
        {
            Assert.That(created, Is.True);
            Assert.That(found, Is.SameAs(movie));
        });
    }

    [Test]
    public async Task CreateAsync_ReturnsFalseWhenIdAlreadyExists()
    {
        var repository = new InMemoryMovieRepository();
        var movie = CreateMovie();

        var firstCreate = await repository.CreateAsync(movie);
        var duplicateCreate = await repository.CreateAsync(movie);

        Assert.Multiple(() =>
        {
            Assert.That(firstCreate, Is.True);
            Assert.That(duplicateCreate, Is.False);
        });
    }

    [Test]
    public async Task UpdateAsync_ReplacesMovieWithMatchingId()
    {
        var repository = new InMemoryMovieRepository();
        var original = CreateMovie();
        var updated = CreateMovie(original.Id, title: "Updated title");
        await repository.CreateAsync(original);

        var result = await repository.UpdateAsync(updated);
        var found = await repository.GetByIdAsync(original.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(found, Is.SameAs(updated));
        });
    }

    [Test]
    public async Task UpdateAsync_ReturnsFalseWhenMovieDoesNotExist()
    {
        var repository = new InMemoryMovieRepository();

        var result = await repository.UpdateAsync(CreateMovie());

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task DeleteAsync_RemovesMovieAndReturnsFalseWhenItIsAlreadyMissing()
    {
        var repository = new InMemoryMovieRepository();
        var movie = CreateMovie();
        await repository.CreateAsync(movie);

        var deleted = await repository.DeleteAsync(movie.Id);
        var found = await repository.GetByIdAsync(movie.Id);
        var deletedAgain = await repository.DeleteAsync(movie.Id);

        Assert.Multiple(() =>
        {
            Assert.That(deleted, Is.True);
            Assert.That(found, Is.Null);
            Assert.That(deletedAgain, Is.False);
        });
    }

    private static Movie CreateMovie(Guid? id = null, string title = "Test movie") =>
        Movie.Create(
            id ?? Guid.NewGuid(),
            title,
            "Description",
            MovieGenre.Drama,
            2020,
            "/images/posters/test.svg").Value!;
}
