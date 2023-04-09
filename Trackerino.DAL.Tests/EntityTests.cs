using System.Linq;
using System.Threading.Tasks;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Factories;
using Xunit;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore.Internal;

namespace Trackerino.DAL.Tests
{
    public class EntityTests : DbContextTestsBase
    {
        [Fact]
        public async Task User_Add()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null
            };

            // Assert
            Assert.Empty(await TrackerinoDbContextSUT.Users.ToListAsync());

            // Act
            await TrackerinoDbContextSUT.Users.AddAsync(user);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var result = await TrackerinoDbContextSUT.Users.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);
        }

        [Fact]
        public async Task User_Delete()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null
            };

            // Act
            await TrackerinoDbContextSUT.Users.AddAsync(user);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var result = await TrackerinoDbContextSUT.Users.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);

            // Remove
            TrackerinoDbContextSUT.Users.Remove(user);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var deleteResult = await TrackerinoDbContextSUT.Users.ToListAsync();

            Assert.Empty(deleteResult);
        }

        [Fact]

        public async Task User_AddProjects()
        {

        }

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
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };

            await TrackerinoDbContextSUT.Users.AddAsync(user);
            await TrackerinoDbContextSUT.Projects.AddAsync(project);
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Arrange
            var activity = new ActivityEntity()
            {
                Id = Guid.Parse(input: "fa3de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                StartDateTime = DateTime.Parse("2023-04-18, 13:53:00"),
                EndDateTime = DateTime.Parse("2023-04-18, 15:23:00"),
                Description = default,
                Project = project,
                Tag = default!,
                User = user
            };

            // Assert
            Assert.Empty(await TrackerinoDbContextSUT.Activities.ToListAsync());

            // Act
            await TrackerinoDbContextSUT.Activities.AddAsync(activity);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var result = await TrackerinoDbContextSUT.Activities.ToListAsync();

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
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };

            await TrackerinoDbContextSUT.Users.AddAsync(user);
            await TrackerinoDbContextSUT.Projects.AddAsync(project);
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Arrange
            var activity = new ActivityEntity()
            {
                Id = Guid.Parse(input: "fa3de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                StartDateTime = DateTime.Parse("2023-04-18, 13:53:00"),
                EndDateTime = DateTime.Parse("2023-04-18, 15:23:00"),
                Description = default,
                Project = project,
                Tag = default!,
                User = user
            };


            // Act
            await TrackerinoDbContextSUT.Activities.AddAsync(activity);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var result = await TrackerinoDbContextSUT.Activities.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(activity, result[0]);

            // Remove
            TrackerinoDbContextSUT.Activities.Remove(activity);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var deleteResult = await TrackerinoDbContextSUT.Activities.ToListAsync();

            Assert.Empty(deleteResult);
        }

        [Fact]
        public async Task Project_Add()
        {
            // Arrange
            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Activities = null,
                Users = null!,
            };

            // Assert
            Assert.Empty(await TrackerinoDbContextSUT.Projects.ToListAsync());

            // Act
            await TrackerinoDbContextSUT.Projects.AddAsync(project);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var result = await TrackerinoDbContextSUT.Projects.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(project, result[0]);
        }

        [Fact]
        public async Task Project_Delete()
        {
            // Arrange
            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Activities = null,
                Users = null!,
            };

            // Act
            await TrackerinoDbContextSUT.Projects.AddAsync(project);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var result = await TrackerinoDbContextSUT.Projects.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(project, result[0]);

            // Remove
            TrackerinoDbContextSUT.Projects.Remove(project);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var deleteResult = await TrackerinoDbContextSUT.Projects.ToListAsync();

            Assert.Empty(deleteResult);
        }
    }
}