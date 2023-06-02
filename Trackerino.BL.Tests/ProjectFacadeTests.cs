using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Trackerino.BL.Facades;
using Trackerino.BL.Models;
using Trackerino.Common.Tests;
using Trackerino.DAL.TestSeeds;


namespace Trackerino.BL.Tests
{
    public sealed class ProjectFacadeTests : FacadeTestsBase
    {
        private readonly IProjectFacade _projectFacadeSUT;

        public ProjectFacadeTests()
        {
            _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
        }

        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            // Arrange
            var model = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Project 1",
                Activities = new ObservableCollection<UserProjectActivityListModel>(),
                Users = new ObservableCollection<ProjectUserListModel>()
            };

            // Act
            var _ = await _projectFacadeSUT.SaveAsync(model);
            //ProjectDetailModel _ = await _projectFacadeSUT.SaveAsync(model);

            // Assert
            /*
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var projectFromDb = await dbxAssert.Projects.FindAsync(model.Id);
            Assert.NotNull(projectFromDb);
            DeepAssert.Equal(model, ProjectModelMapper.MapToDetailModel(projectFromDb));
            */
        }

        [Fact]
        public async Task GetAll_Single_SeededProject()
        {
            // Arrange
            var projects = await _projectFacadeSUT.GetAsync();
            var project = projects.Single(p => p.Id == TestProjectSeeds.ProjectEntity.Id);

            // Assert
            DeepAssert.Equal(ProjectModelMapper.MapToListModel(TestProjectSeeds.ProjectEntity), project);
        }


        [Fact]
        public async Task GetById_NonExistent()
        {
            // Arrange
            var project = await _projectFacadeSUT.GetAsync(TestProjectSeeds.EmptyProjectEntity.Id);

            // Assert
            Assert.Null(project);
        }

        [Fact]
        public async Task SeededProject_DeleteById_Deleted()
        {
            // Act
            await _projectFacadeSUT.DeleteAsync(TestProjectSeeds.ProjectEntityDelete.Id);

            // Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Projects.AnyAsync(p => p.Id == TestProjectSeeds.ProjectEntityDelete.Id));
        }

        [Fact]
        public async Task Delete_ProjectWithActivities_Throws()
        {
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _projectFacadeSUT.DeleteAsync(TestProjectSeeds.ProjectEntity.Id));
        }
        [Fact]
        public async Task Delete_NonExistent_Project_Throws()
        {
            //Arrange
            var nonExistentProjectId = Guid.NewGuid();

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _projectFacadeSUT.DeleteAsync(nonExistentProjectId));
        }

        [Fact]
        public async Task NewProject_InsertOrUpdate_ProjectAdded()
        {
            // Arrange
            var model = new ProjectDetailModel()
            {
                Id = Guid.Empty,
                Name = "Project 1",
                Activities = new ObservableCollection<UserProjectActivityListModel>(),
                Users = new ObservableCollection<ProjectUserListModel>()
            };

            // Act
            model = await _projectFacadeSUT.SaveAsync(model);

            // Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var projectFromDb = await dbxAssert.Projects.SingleAsync(p => p.Id == model.Id);
            DeepAssert.Equal(model, ProjectModelMapper.MapToDetailModel(projectFromDb));
        }
    }
}