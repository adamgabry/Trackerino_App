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
        public async Task CreateUser_NotThrowingException()
        {
            // Arrange
            var model = new ActivityDetailModel()
            {
                Id = Guid.Empty,
                StartDateTime = DateTime.UtcNow,
                EndDateTime = DateTime.UtcNow.AddHours(2),
                Tag = ActivityTag.Work,
                Description = "Test activity",
                User = new UserListModel() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" },
                Project = new ProjectListModel() { Id = Guid.NewGuid(), Name = "Test project" }
            };

            // Act
            var result = await _activityFacadeSUT.SaveAsync(model);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(model.StartDateTime, result.StartDateTime);
            Assert.Equal(model.EndDateTime, result.EndDateTime);
            Assert.Equal(model.Tag, result.Tag);
            Assert.Equal(model.Description, result.Description);
            Assert.NotNull(result.User);
            Assert.Equal(model.User?.Id, result.User.Id);
            Assert.Equal(model.User?.Name, result.User.Name);
            Assert.NotNull(result.Project);
            Assert.Equal(model.Project?.Id, result.Project.Id);
            Assert.Equal(model.Project?.Name, result.Project.Name);

        }


    }
}