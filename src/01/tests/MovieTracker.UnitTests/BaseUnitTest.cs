using Bogus;

namespace MovieTracker.UnitTests;

public abstract class BaseUnitTest
{
    private const int FakerSeed = 1337;

    protected BaseUnitTest()
    {
        Faker = new Faker("en")
        {
            Random = new Randomizer(FakerSeed)
        };
    }

    protected Faker Faker { get; }
}
