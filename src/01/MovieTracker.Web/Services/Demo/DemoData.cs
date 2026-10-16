using MovieTracker.Web.Models.Demo;

namespace MovieTracker.Web.Services.Demo;

public static class DemoData
{
    public static IReadOnlyList<DemoMovie> Movies { get; } =
    [
        new(Guid.Parse("00000000-0000-0000-0000-000000000001"), "Interstellar", 2014, "Science Fiction", 169, 8.7m, "/images/posters/interstellar.svg", "Explorers cross the stars in search of a future home for humanity."),
        new(Guid.Parse("00000000-0000-0000-0000-000000000002"), "Inception", 2010, "Science Fiction", 148, 8.8m, "/images/posters/inception.svg", "A specialist enters layered dreams to plant an idea in a guarded mind."),
        new(Guid.Parse("00000000-0000-0000-0000-000000000003"), "Arrival", 2016, "Science Fiction", 116, 7.9m, "/images/posters/arrival.svg", "A linguist races to understand mysterious visitors before fear takes over."),
        new(Guid.Parse("00000000-0000-0000-0000-000000000006"), "Whiplash", 2014, "Drama", 107, 8.5m, "/images/posters/whiplash.svg", "An ambitious drummer is pushed to extremes by a demanding instructor."),
        new(Guid.Parse("00000000-0000-0000-0000-000000000007"), "Parasite", 2019, "Thriller", 132, 8.5m, "/images/posters/parasite.svg", "Two families become entangled through opportunity, deception, and class tension.")
    ];

    public static IReadOnlyList<DemoRating> Ratings { get; } =
    [
        new("Interstellar", 9.0m, new DateOnly(2026, 8, 9)),
        new("Whiplash", 8.5m, new DateOnly(2026, 7, 26)),
        new("Arrival", 8.0m, new DateOnly(2026, 7, 12)),
        new("Parasite", 9.0m, new DateOnly(2026, 6, 30))
    ];
}
