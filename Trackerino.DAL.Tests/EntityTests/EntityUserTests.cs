using Trackerino.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.Tests.EntityTests
{
    public class EntityUserTests : DbContextTestsBase
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

    }
}