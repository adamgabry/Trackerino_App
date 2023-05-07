using Trackerino.BL.Facades;
using Trackerino.BL.Models;
using Trackerino.Common.Tests;
using Trackerino.DAL.Common;
using Trackerino.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Trackerino.BL.Facades.Interfaces;

namespace Trackerino.BL.Tests
{
    public sealed class ActivityFacadeTests : FacadeTestsBase
    {
        private readonly IActivityFacade _activityFacadeSUT;

        public ActivityFacadeTests(ITestOutputHelper output) : base(output)
        {
            _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
        }

        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new ActivityDetailModel()
            {
                User = UserListModel.Empty,
                Project = ProjectListModel.Empty,
                Id = Guid.Empty,
                Description = @"Test Description",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Tag = ActivityTag.Work
            };

            var _ = await _activityFacadeSUT.SaveAsync(model);
        }

        [Fact]
        public async Task GetAll_Single_SeededActivity()
        {
            var activities = await _activityFacadeSUT.GetAsync();
            var activity = activities.Single(i => i.Id == ActivitySeeds.ActivityEntity1.Id);

            DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity1), activity);
        }

        [Fact]
        public async Task GetById_SeededActivity()
        {
            var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.ActivityEntity2.Id);

            DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity2), activity);
        }

        [Fact]
        public async Task GetById_NonExistent()
        {
            var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivityEntity.Id);

            Assert.Null(activity);
        }

        [Fact]
        public async Task SeededActivity_DeleteById_Deleted()
        {
            await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity2.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.ActivityEntity2.Id));
        }

        [Fact]
        public async Task Delete_ActivityUsedInTrackerEntry_Throws()
        {
            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity1.Id));
        }

        [Fact]
        public async Task NewActivity_InsertOrUpdate_ActivityAdded()
        {
            //Arrange
            var activity = new ActivityDetailModel()
            {
                Id = Guid.Empty,
                User = UserListModel.Empty,
                Project = ProjectListModel.Empty,
                Description = "Test Description",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Tag = ActivityTag.Work
            };

            //Act
            activity = await _activityFacadeSUT.SaveAsync(activity);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
            DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
        }
    }
}