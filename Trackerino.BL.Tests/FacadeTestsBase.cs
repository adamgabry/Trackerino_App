using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Trackerino.BL.Mappers;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.DAL;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.UnitOfWork;
using Trackerino.DAL.Factories;

namespace Trackerino.BL.Tests
{
    public class FacadeTestsBase : IAsyncLifetime
    {
        protected FacadeTestsBase()
        {
            DbContextFactory = new DbContextLocalDbFactory(GetType().FullName!, seedDemoData: true);

            ActivityEntityMapper = new ActivityEntityMapper();
            ProjectEntityMapper = new ProjectEntityMapper();
            UserEntityMapper = new UserEntityMapper();
            UserProjectEntityMapper = new UserProjectEntityMapper();

            ActivityModelMapper = new ActivityModelMapper(UserModelMapper, ProjectModelMapper);
            ProjectModelMapper = new ProjectModelMapper(ProjectUserModelMapper, UserProjectActivityModelMapper);
            UserModelMapper = new UserModelMapper(UserProjectActivityModelMapper, UserProjectModelMapper);
            UserProjectModelMapper = new UserProjectModelMapper();
            ProjectUserModelMapper = new ProjectUserModelMapper();

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
        protected IProjectUserModelMapper ProjectUserModelMapper { get; }
        protected IUserProjectActivityModelMapper UserProjectActivityModelMapper { get; }
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