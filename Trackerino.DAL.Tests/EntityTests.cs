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

    }
}