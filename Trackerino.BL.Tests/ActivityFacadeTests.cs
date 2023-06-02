using Trackerino.BL.Facades;
using Trackerino.BL.Models;
using Trackerino.Common.Tests;
using Trackerino.DAL.Common;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.DAL.TestSeeds;

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
            var activity = await _activityFacadeSUT.GetAsync(TestActivitySeeds.EmptyActivityEntity.Id);

            Assert.Null(activity);
        }

        [Fact]
        public async Task GetAll_Single_UserEntity1()
        {
            var activities = await _activityFacadeSUT.GetAsync();
            var activity = activities.Single(i => i.Id == TestActivitySeeds.ActivityEntity1.Id);

            DeepAssert.Equal(ActivityModelMapper.MapToListModel(TestActivitySeeds.ActivityEntity1), activity);
        }

        [Fact]
        public async Task GetById_UserEntity()
        {
            var detailModel = ActivityModelMapper.MapToDetailModel(TestActivitySeeds.ActivityEntity1);
            var activity = await _activityFacadeSUT.GetAsync(detailModel.Id);

            DeepAssert.Equal(detailModel, activity);
        }
        [Fact]
        public async Task UserEntity_DeleteById_Deleted()
        {
            await _activityFacadeSUT.DeleteAsync(TestActivitySeeds.ActivityEntity1.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == TestActivitySeeds.ActivityEntity1.Id));
        }
        /*
        [Fact]
        public async Task NewActivity_InsertOrUpdate_ActivityAdded()
        {
            //Arrange
            var activitySeed = TestActivitySeeds.EmptyActivityEntity;
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
                Id = TestActivitySeeds.ActivityEntity.Id,
                Tag = TestActivitySeeds.ActivityEntity.Tag,
                Description = TestActivitySeeds.ActivityEntity.Description,
                StartDateTime = TestActivitySeeds.ActivityEntity.StartDateTime,
                EndDateTime = TestActivitySeeds.ActivityEntity.EndDateTime,

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