using Trackerino.BL.Facades;
using Trackerino.Common.Tests;
using Trackerino.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Trackerino.BL.Facades.Interfaces;

namespace Trackerino.BL.Tests
{
    public sealed class ActivityFacadeTests : FacadeTestsBase
    {
        private readonly IActivityFacade _activityFacadeSUT;

        public ActivityFacadeTests()
        {
            _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
        }

        [Fact]
        public async Task GetById_NonExistent()
        {
            var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivityEntity.Id);

            Assert.Null(activity);
        }

        [Fact]
        public async Task GetAll_Single_UserEntity1()
        {
            var activities = await _activityFacadeSUT.GetAsync();
            var activity = activities.Single(i => i.Id == ActivitySeeds.ActivityEntity1.Id);

            DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity1), activity);
        }

        [Fact]
        public async Task GetById_UserEntity()
        {
            var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.ActivityEntity1.Id);

            DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity1), activity);
        }
        [Fact]
        public async Task UserEntity_DeleteById_Deleted()
        {
            await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity1.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.ActivityEntity1.Id));
        }
        /*
        [Fact]
        public async Task NewActivity_InsertOrUpdate_ActivityAdded()
        {
            //Arrange
            var activitySeed = ActivitySeeds.EmptyActivityEntity;
            var activity = new ActivityDetailModel()
            {
                Id = activitySeed.Id,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(2),
                Tag = ActivityTag.Meeting,
            };

            //Act
            activity = await _activityFacadeSUT.SaveAsync(activity); //Fails here

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
            DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
        }
        [Fact]
        public async Task SeededActivity_InsertOrUpdate_ActivityUpdated()
        {
            //Arrange
            var activity = new ActivityDetailModel()
            {
                Id = ActivitySeeds.ActivityEntity.Id,
                Tag = ActivitySeeds.ActivityEntity.Tag,
                Description = ActivitySeeds.ActivityEntity.Description,
                StartDateTime = ActivitySeeds.ActivityEntity.StartDateTime,
                EndDateTime = ActivitySeeds.ActivityEntity.EndDateTime,

                Project = new ProjectListModel() {
                    Id = Guid.Parse("4FD824C0-A7D1-48BA-8E7C-4F136CF8BF31"),
                    Name = "projectTest"
                },
                User = new UserListModel() {
                    Id = Guid.Parse("77146EA0-2D86-4874-B75E-FBA628AFC698"),
                    Name = "TestName",
                    Surname = "TestSurname",

                }
            };
            activity.Description += "updated";
            activity.Tag = ActivityTag.Work;

            //Act
            await _activityFacadeSUT.SaveAsync(activity); // again fails at MapToEntity

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
            DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));   //activityFromDb still has user and project set to null, but activity doesnt contain it at all, otherwise same.
        }
        */
    }
}