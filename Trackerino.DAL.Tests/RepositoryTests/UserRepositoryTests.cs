using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Trackerino.DAL.Tests.RepositoryTests
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
            var repository = new Repository<UserEntity>(TrackerinoDbContextSut, new UserEntityMapper());

            // Act
            await repository.InsertAsync(user);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Assertprepare
            var result = await TrackerinoDbContextSut.Users.ToListAsync();

            // Teardown
            repository.Delete(user.Id);
            await TrackerinoDbContextSut.SaveChangesAsync();

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
            var repository = new Repository<UserEntity>(TrackerinoDbContextSut, new UserEntityMapper());
            await repository.InsertAsync(user);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Check that user exists in repository
            bool userExists = repository.Get().Any(u => u.Id == user.Id);
            await TrackerinoDbContextSut.SaveChangesAsync();
            Assert.True(userExists);

            // Modify user and call UpdateAsync()
            user.Name = "Pepa Updated";
            user.Surname = "Novak Updated";
            await repository.UpdateAsync(user);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Assertprepare
            var result = await TrackerinoDbContextSut.Users.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);

            // Teardown
            repository.Delete(user.Id);
            await TrackerinoDbContextSut.SaveChangesAsync();
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
            var repository = new Repository<UserEntity>(TrackerinoDbContextSut, new UserEntityMapper());
            await TrackerinoDbContextSut.Users.AddAsync(user);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Act
            repository.Delete(user.Id);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Assert
            var result = await TrackerinoDbContextSut.Users.ToListAsync();
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
            var repository = new Repository<UserEntity>(TrackerinoDbContextSut, new UserEntityMapper());
            await repository.InsertAsync(user);

            // Act
            await TrackerinoDbContextSut.SaveChangesAsync();
            bool result = await repository.ExistsAsync(user);

            // Assert
            Assert.True(result);

            // Teardown
            repository.Delete(user.Id);
            await TrackerinoDbContextSut.SaveChangesAsync();
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
            var repository = new Repository<UserEntity>(TrackerinoDbContextSut, new UserEntityMapper());
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Act
            bool result = await repository.ExistsAsync(user);
            await TrackerinoDbContextSut.SaveChangesAsync();

            // Assert
            Assert.False(result);
            await TrackerinoDbContextSut.SaveChangesAsync();
        }
    }
}