namespace Trackerino.DAL.Tests
{
    public class DbContextBaseTest : IAsyncLifetime
    {
        [Fact]
        public void Test1()
        {

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
}