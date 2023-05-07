using Trackerino.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.Tests.EntityTests
{
    public class EntityUserProjectTests : DbContextTestsBase
    {
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
    }

}