using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Trackerino.BL.Mappers;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.DAL;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.UnitOfWork;

namespace Trackerino.BL.Tests
{
    public class FacadeTestsBase : IAsyncLifetime
    {
        protected FacadeTestsBase(ITestOutputHelper output)
        {
            // XUnitTestOutputConverter

            DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);

            ActivityEntityMapper = new ActivityEntityMapper();
            ProjectEntityMapper = new ProjectEntityMapper();
            UserEntityMapper = new UserEntityMapper();
            UserProjectEntityMapper = new UserProjectEntityMapper();

            ActivityModelMapper = new ActivityModelMapper(UserModelMapper, ProjectModelMapper);
            ProjectModelMapper = new ProjectModelMapper(UserProjectModelMapper, ActivityModelMapper);
            UserModelMapper = new UserModelMapper(ActivityModelMapper, UserProjectModelMapper);
            UserProjectModelMapper = new UserProjectModelMapper();

            UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
        }

        protected IDbContextFactory<TrackerinoDbContext> DbContextFactory { get; }

        protected ActivityEntityMapper ActivityEntityMapper { get; }
        protected ProjectEntityMapper ProjectEntityMapper { get; }
        protected UserEntityMapper UserEntityMapper { get; }
        protected UserProjectEntityMapper UserProjectEntityMapper { get; }

        protected IActivityModelMapper ActivityModelMapper { get; }
        protected IProjectModelMapper ProjectModelMapper { get; }
        protected IUserModelMapper UserModelMapper { get; }
        protected IUserProjectModelMapper UserProjectModelMapper { get; }
        protected IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public async Task InitializeAsync()
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            await dbx.Database.EnsureDeletedAsync();
            await dbx.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            await dbx.Database.EnsureDeletedAsync();
        }
    }
}