using Trackerino.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Trackerino.Common.Tests;
using Trackerino.Common.Tests.Seeds;

namespace Trackerino.DAL.Tests.EntityTests
{
    public class EntityProjectTests : DbContextTestsBase
    {
        [Fact]
        public async Task Project_Add()
        {
            // Arrange
            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name"
            };

            // Act
            TrackerinoDbContextSut.Projects.Add(project);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Assert
            await using var dbx = await TrackerinoDbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Projects
                .SingleAsync(i => i.Id == project.Id);
            DeepAssert.Equal(project, actualEntity);
        }

        [Fact]
        public async Task Project_Delete()
        {
            // Arrange
            var project = new ProjectEntity
            {
                Id = Guid.Parse(input: "fa0de0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                Name = "Name",
            };

            // Act
            TrackerinoDbContextSut.Projects.Add(project);
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