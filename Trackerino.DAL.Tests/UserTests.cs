using System.Linq;
using System.Threading.Tasks;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Factories;
using Xunit;
using System.Threading.Channels;

namespace Trackerino.DAL.Tests
{
    public class UserRepositoryTests : DbContextTestsBase
    {
        [Fact]
        public async Task NewUser_InsertAsync_Inserted()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Pepa",
                Surname = "Novak",
            };
            var repository = new Repository<UserEntity>(TrackerinoDbContextSUT, new UserEntityMapper());

            // Act
            await repository.InsertAsync(user);
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Assertprepare
            var result = await TrackerinoDbContextSUT.Users.ToListAsync();

            // Teardown
            repository.Delete(user.Id);
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);
        }

        [Fact]
        public async Task ExistingUser_UpdateAsync_Updated()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Pepa",
                Surname = "Novak",
            };
            var repository = new Repository<UserEntity>(TrackerinoDbContextSUT, new UserEntityMapper());
            await repository.InsertAsync(user);
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Check that user exists in repository
            var userExists = repository.Get().Any(u => u.Id == user.Id);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            Assert.True(userExists);

            // Modify user and call UpdateAsync()
            user.Name = "Pepa Updated";
            user.Surname = "Novak Updated";
            await repository.UpdateAsync(user);
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Assertprepare
            var result = await TrackerinoDbContextSUT.Users.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);

            // Teardown
            repository.Delete(user.Id);
            await TrackerinoDbContextSUT.SaveChangesAsync();
        }

        [Fact]
        public async Task ExistingUser_Delete_Deleted()
        {
        // Arrange
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "Pepa",
            Surname = "Novak",
        };
        var repository = new Repository<UserEntity>(TrackerinoDbContextSUT, new UserEntityMapper());
        await TrackerinoDbContextSUT.Users.AddAsync(user);
        await TrackerinoDbContextSUT.SaveChangesAsync();

        // Act
        repository.Delete(user.Id);
        await TrackerinoDbContextSUT.SaveChangesAsync();

        // Assert
        var result = await TrackerinoDbContextSUT.Users.ToListAsync();
        Assert.Empty(result);
    }

        [Fact]
        public async Task ExistingUser_ExistsAsync_ReturnsTrue()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Pepa",
                Surname = "Novak",
            };
            var repository = new Repository<UserEntity>(TrackerinoDbContextSUT, new UserEntityMapper());
            await repository.InsertAsync(user);

            // Act
            await TrackerinoDbContextSUT.SaveChangesAsync();
            var result = await repository.ExistsAsync(user);

            // Assert
            Assert.True(result);

            // Teardown
            repository.Delete(user.Id);
            await TrackerinoDbContextSUT.SaveChangesAsync();
            }

        [Fact]
        public async Task NonexistentUser_ExistsAsync_ReturnsFalse()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Pepa",
                Surname = "Novak",
            };
            var repository = new Repository<UserEntity>(TrackerinoDbContextSUT, new UserEntityMapper());
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Act
            var result = await repository.ExistsAsync(user);
            await TrackerinoDbContextSUT.SaveChangesAsync();

            // Assert
            Assert.False(result);
            await TrackerinoDbContextSUT.SaveChangesAsync();
        }
    }
}