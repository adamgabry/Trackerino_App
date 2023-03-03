using Xunit.Abstractions;
namespace Trackerino.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    ///NOT implemented
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        throw new NotImplementedException();
    }

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}