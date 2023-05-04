using Trackerino.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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
                Surname = "Surname"
            };

            // Assert
            Assert.Empty(await TrackerinoDbContextSut.Users.ToListAsync());

            // Act
            await TrackerinoDbContextSut.Users.AddAsync(user);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var result = await TrackerinoDbContextSut.Users.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);
        }

        [Fact]
        public async Task User_AddMany()
        {
            // Arrange
            for (int i = 0; i < 5; i++)
            {
                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Surname = "Surname"
                };

                await TrackerinoDbContextSut.Users.AddAsync(user);
            }

            // Act
            await TrackerinoDbContextSut.SaveChangesAsync();
            var result = await TrackerinoDbContextSut.Users.ToListAsync();

            // Assert
            Assert.Equal(5, result.Count);
        }


        [Fact]
        public async Task User_Delete()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname"
            };

            // Act
            await TrackerinoDbContextSut.Users.AddAsync(user);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var result = await TrackerinoDbContextSut.Users.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);

            // Remove
            TrackerinoDbContextSut.Users.Remove(user);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var deleteResult = await TrackerinoDbContextSut.Users.ToListAsync();

            Assert.Empty(deleteResult);
        }

        [Fact]

        public async Task UserProject_Add()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };

            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Project",
                Activities = null,
                Users = null!,
            };

            var userProject = new UserProjectEntity
            {
                Id = Guid.Parse(input: "da0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                ProjectId = project.Id,
                Project = project,
                UserId = user.Id,
                User = user
            };

            await TrackerinoDbContextSut.Users.AddAsync(user);
            await TrackerinoDbContextSut.Projects.AddAsync(project);
            await TrackerinoDbContextSut.UserProject.AddAsync(userProject);
            await TrackerinoDbContextSut.SaveChangesAsync();

            var result = await TrackerinoDbContextSut.UserProject.ToListAsync();

            Assert.Single(result);
            Assert.Equal(userProject, result[0]);

            Assert.Equal(result[0].ProjectId, project.Id);
            Assert.Equal(result[0].UserId, user.Id);
        }

        [Fact]
        public async Task UserProject_AddMultipleUsers()
        {
            // Arrange
            var user1 = new UserEntity
            {
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };

            var user2 = new UserEntity
            {
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cd2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };

            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Project",
                Activities = null,
                Users = null!,
            };

            var user1Project = new UserProjectEntity
            {
                Id = Guid.Parse(input: "da0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                ProjectId = project.Id,
                Project = project,
                UserId = user1.Id,
                User = user1
            };

            var user2Project = new UserProjectEntity
            {
                Id = Guid.Parse(input: "da0de0cd-eefe-443f-b3f6-3d96cc2cbf2e"),
                ProjectId = project.Id,
                Project = project,
                UserId = user2.Id,
                User = user2
            };

            await TrackerinoDbContextSut.Users.AddAsync(user1);
            await TrackerinoDbContextSut.Users.AddAsync(user2);
            await TrackerinoDbContextSut.Projects.AddAsync(project);
            await TrackerinoDbContextSut.UserProject.AddAsync(user1Project);
            await TrackerinoDbContextSut.UserProject.AddAsync(user2Project);
            await TrackerinoDbContextSut.SaveChangesAsync();

            var result = await TrackerinoDbContextSut.UserProject.ToListAsync();

            Assert.Equal(2, result.Count);

            Assert.Equal(result[0].ProjectId, result[1].ProjectId);
            Assert.NotEqual(result[0].UserId, result[1].UserId);
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

            await TrackerinoDbContextSut.Users.AddAsync(user);
            await TrackerinoDbContextSut.Projects.AddAsync(project);
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
                User = user
            };

            // Assert
            Assert.Empty(await TrackerinoDbContextSut.Activities.ToListAsync());

            // Act
            await TrackerinoDbContextSut.Activities.AddAsync(activity);
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
                Id = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
                Surname = "Surname",
                Projects = null!
            };

            await TrackerinoDbContextSut.Users.AddAsync(user);
            await TrackerinoDbContextSut.Projects.AddAsync(project);
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
                User = user
            };


            // Act
            await TrackerinoDbContextSut.Activities.AddAsync(activity);
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
            Assert.Empty(await TrackerinoDbContextSut.Projects.ToListAsync());

            // Act
            await TrackerinoDbContextSut.Projects.AddAsync(project);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var result = await TrackerinoDbContextSut.Projects.ToListAsync();

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
            await TrackerinoDbContextSut.Projects.AddAsync(project);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var result = await TrackerinoDbContextSut.Projects.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(project, result[0]);

            // Remove
            TrackerinoDbContextSut.Projects.Remove(project);
            await TrackerinoDbContextSut.SaveChangesAsync();
            var deleteResult = await TrackerinoDbContextSut.Projects.ToListAsync();

            Assert.Empty(deleteResult);
        }
    }
}