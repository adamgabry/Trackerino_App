using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Trackerino.BL.Mappers;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.Common.Tests;
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
            DbContextFactory = new DbContextLocalDbFactory(GetType().FullName!, seedTestingData: true);

            UserProjectModelMapper = new UserProjectModelMapper();
            ProjectUserModelMapper = new ProjectUserModelMapper();
            UserProjectActivityModelMapper = new UserProjectActivityModelMapper();
            ProjectModelMapper = new ProjectModelMapper(ProjectUserModelMapper, UserProjectActivityModelMapper);
            UserModelMapper = new UserModelMapper(UserProjectActivityModelMapper, UserProjectModelMapper);
            ActivityModelMapper = new ActivityModelMapper(UserModelMapper, ProjectModelMapper);
            
            

            UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
        }

        protected IDbContextFactory<TrackerinoDbContext> DbContextFactory { get; }
        
        protected ActivityModelMapper ActivityModelMapper { get; }
        protected ProjectModelMapper ProjectModelMapper { get; }
        protected UserModelMapper UserModelMapper { get; }
        protected UserProjectModelMapper UserProjectModelMapper { get; }
        protected ProjectUserModelMapper ProjectUserModelMapper { get; }
        protected UserProjectActivityModelMapper UserProjectActivityModelMapper { get; }
        protected UnitOfWorkFactory UnitOfWorkFactory { get; }

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