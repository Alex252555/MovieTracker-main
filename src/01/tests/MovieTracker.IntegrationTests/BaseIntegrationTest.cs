using Microsoft.AspNetCore.Mvc.Testing;

namespace MovieTracker.IntegrationTests;

public abstract class BaseIntegrationTest
{
    protected IntegrationTestWebAppFactory Factory { get; private set; } = null!;
    protected HttpClient Client { get; private set; } = null!;

    [OneTimeSetUp]
    public void BaseOneTimeSetUp()
    {
        Factory = new IntegrationTestWebAppFactory();
        Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    [OneTimeTearDown]
    public void BaseOneTimeTearDown()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}
