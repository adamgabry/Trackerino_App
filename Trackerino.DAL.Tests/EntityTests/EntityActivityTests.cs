using Trackerino.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.Tests.EntityTests
{
    public class EntityActivityTests : DbContextTestsBase
    {
        [Fact]
        public async Task Activity_Add()
        {
            // Arrange
            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Activities = null,
                Users = null!,
            };

            var user = new UserEntity
            {
                Id = Guid.Parse(input: "800961ae-395b-4048-9576-96d952a6dc7c"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };
            
            TrackerinoDbContextSut.Users.Add(user);
            TrackerinoDbContextSut.Projects.Add(project);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Arrange
            var activity = new ActivityEntity()
            {
                Id = Guid.Parse(input: "fa3de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                StartDateTime = DateTime.Parse("2023-04-18, 13:53:00"),
                EndDateTime = DateTime.Parse("2023-04-18, 15:23:00"),
                Description = default,
                Project = project,
                Tag = default!,
                UserId = user.Id,
                ProjectId = project.Id
            };

            // Assert
            Assert.Empty(await TrackerinoDbContextSut.Activities.ToListAsync());

            // Act
            TrackerinoDbContextSut.Activities.Add(activity);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var result = await TrackerinoDbContextSut.Activities.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(activity, result[0]);
        }

        [Fact]
        public async Task Activity_Delete()
        {
            // Arrange
            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Activities = null,
                Users = null!,
            };

            var user = new UserEntity
            {
                Id = Guid.Parse(input: "800961ae-395b-4048-9576-96d952a6dc7c"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };

            TrackerinoDbContextSut.Users.Add(user);
            TrackerinoDbContextSut.Projects.Add(project);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Arrange
            var activity = new ActivityEntity()
            {
                Id = Guid.Parse(input: "fa3de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                StartDateTime = DateTime.Parse("2023-04-18, 13:53:00"),
                EndDateTime = DateTime.Parse("2023-04-18, 15:23:00"),
                Description = default,
                Project = project,
                Tag = default!,
                UserId = user.Id,
                ProjectId = project.Id
            };


            // Act
            TrackerinoDbContextSut.Activities.Add(activity);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var result = await TrackerinoDbContextSut.Activities.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(activity, result[0]);

            // Remove
            TrackerinoDbContextSut.Activities.Remove(activity);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var deleteResult = await TrackerinoDbContextSut.Activities.ToListAsync();

            Assert.Empty(deleteResult);
        }

    }
}