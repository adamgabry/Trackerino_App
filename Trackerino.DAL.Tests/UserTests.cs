using System.Linq;
using System.Threading.Tasks;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Channels;

namespace Trackerino.DAL.Tests
{
    public class UserRepositoryTests
    {
        private readonly TrackerinoDbContext _dbContextSUT;

        public UserRepositoryTests()
        {
            _dbContextSUT = new TrackerinoDbContext(new DbContextOptionsBuilder<TrackerinoDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options);
        }

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
            var repository = new Repository<UserEntity>(_dbContextSUT, new UserEntityMapper());

            // Act
            await repository.InsertAsync(user);
            await _dbContextSUT.SaveChangesAsync();

            // Assertprepare
            var result = await _dbContextSUT.Users.ToListAsync();

            // Teardown
            repository.Delete(user.Id);
            await _dbContextSUT.SaveChangesAsync();

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
            var repository = new Repository<UserEntity>(_dbContextSUT, new UserEntityMapper());
            await repository.InsertAsync(user);
            await _dbContextSUT.SaveChangesAsync();

            // Check that user exists in repository
            var userExists = repository.Get().Any(u => u.Id == user.Id);
            await _dbContextSUT.SaveChangesAsync();
            Assert.True(userExists);

            // Modify user and call UpdateAsync()
            user.Name = "Pepa Updated";
            user.Surname = "Novak Updated";
            await repository.UpdateAsync(user);
            await _dbContextSUT.SaveChangesAsync();

            // Assertprepare
            var result = await _dbContextSUT.Users.ToListAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(user, result[0]);

            // Teardown
            repository.Delete(user.Id);
            await _dbContextSUT.SaveChangesAsync();
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
        var repository = new Repository<UserEntity>(_dbContextSUT, new UserEntityMapper());
        await _dbContextSUT.Users.AddAsync(user);
        await _dbContextSUT.SaveChangesAsync();

        // Act
        repository.Delete(user.Id);
        await _dbContextSUT.SaveChangesAsync();

        // Assert
        var result = await _dbContextSUT.Users.ToListAsync();
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
            var repository = new Repository<UserEntity>(_dbContextSUT, new UserEntityMapper());
            await repository.InsertAsync(user);

            // Act
            await _dbContextSUT.SaveChangesAsync();
            var result = await repository.ExistsAsync(user);

            // Assert
            Assert.True(result);

            // Teardown
            repository.Delete(user.Id);
            await _dbContextSUT.SaveChangesAsync();
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
            var repository = new Repository<UserEntity>(_dbContextSUT, new UserEntityMapper());
            await _dbContextSUT.SaveChangesAsync();

            // Act
            var result = await repository.ExistsAsync(user);
            await _dbContextSUT.SaveChangesAsync();

            // Assert
            Assert.False(result);
            await _dbContextSUT.SaveChangesAsync();
        }
    }
}