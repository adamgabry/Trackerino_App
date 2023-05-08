using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Trackerino.BL.Facades;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Mappers;
using Trackerino.BL.Models;
using Trackerino.BL.Tests;
using Trackerino.Common.Tests;
using Trackerino.Common.Tests.Seeds;
using Trackerino.DAL.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace Trackerino.BL.Tests.Facades
{
    public sealed class UserProjectFacadeTests : FacadeTestsBase
    {
        private readonly IUserProjectFacade _userProjectFacadeSUT;

        public UserProjectFacadeTests()
        {
            _userProjectFacadeSUT = new UserProjectFacade(UnitOfWorkFactory, UserProjectModelMapper);
        }

        [Fact]
        public async Task SaveAsync_NewUserProject_UserProjectAdded()
        {
            // Arrange
            var userProject = new UserProjectDetailModel()
            {
                Id = UserSeeds.UserEntity1.Id,
                ProjectId = ProjectSeeds.ProjectEntity.Id,
                ProjectName = ProjectSeeds.ProjectEntity.Name
            };
            var userId = UserSeeds.UserEntity1.Id;

            // Act
            await _userProjectFacadeSUT.SaveAsync(userProject, userId);

            // Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userProjectFromDb = await dbxAssert.UserProject.SingleOrDefaultAsync(up => up.ProjectId == userProject.ProjectId && up.UserId == userId);
            Assert.NotNull(userProjectFromDb);
            DeepAssert.Equal(userProject, UserProjectModelMapper.MapToDetailModel(userProjectFromDb));
        }

        [Fact]
        public async Task SaveAsync_ExistingUserProject_UserProjectUpdated()
        {
            // Arrange
            var userProjectFromDb = UserProjectSeeds.UserProjectEntity1;
            var updatedUserProject = new UserProjectDetailModel()
            {
                Id = userProjectFromDb.Id,
                ProjectId = userProjectFromDb.ProjectId,
                ProjectName = ProjectSeeds.ProjectEntity.Name
            };
            var userId = UserSeeds.UserEntity1.Id;

            // Act
            await _userProjectFacadeSUT.SaveAsync(updatedUserProject, userId);

            // Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            userProjectFromDb = await dbxAssert.UserProject
                .SingleOrDefaultAsync(up => up.ProjectId == updatedUserProject.ProjectId && up.UserId == userId);
            Assert.NotNull(userProjectFromDb);
            DeepAssert.Equal(updatedUserProject, UserProjectModelMapper.MapToDetailModel(userProjectFromDb)); //all the params are identical but Project and User is null at userProjectFromDb
        }

        [Fact]
        public async Task DeleteAsync_ExistingUserProject_UserProjectDeleted()
        {
            // Arrange
            var userProjectFromDb = UserProjectSeeds.UserProjectEntity1;
            var userId = UserSeeds.UserEntity1.Id;

            // Act
            await _userProjectFacadeSUT.DeleteAsync(userProjectFromDb.Id);

            // Assert
            //TODO: maybe wrong Dbcontext? - how to get Common? 
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            userProjectFromDb = await dbxAssert.UserProject
                .SingleOrDefaultAsync(up => up.ProjectId == userProjectFromDb.ProjectId && up.UserId == userId);
            Assert.Null(userProjectFromDb);
        }
    }
}
