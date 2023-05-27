using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Facades;
using Trackerino.BL.Mappers;
using Trackerino.BL.Models;
using Trackerino.BL.Tests;
using Trackerino.Common.Tests.Seeds;
using Trackerino.Common.Tests;
using Trackerino.DAL.UnitOfWork;
using Xunit.Abstractions;

/*      UserEntityWithNoActivities,
        UserEntityWithNoProjects,
        UserEntity1,
        UserEntity2,
        UserEntity,
        UserEntityUpdate,
        UserEntityDelete);
*/
public sealed class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;

    public UserFacadeTests() : base()
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = "John",
            Surname = "Doe",
            ImageUrl = null,
            Projects = new ObservableCollection<UserProjectListModel>(),
            Activities = new ObservableCollection<UserProjectActivityListModel>()
        };

        var _ = await _userFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_User()
    {
        var users = await _userFacadeSUT.GetAsync();
        var user = users.Single(u => u.Id == UserSeeds.UserEntity1.Id);

        DeepAssert.Equal(UserModelMapper.MapToListModel(UserSeeds.UserEntity1), user);
    }

    [Fact]
    public async Task GetById_SeededUser()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.UserEntity1.Id);

        DeepAssert.Equal(UserModelMapper.MapToDetailModel(UserSeeds.UserEntity1), user);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.EmptyUserEntity.Id);

        Assert.Null(user);
    }

    [Fact]
    public async Task SeededUser_DeleteById_Deleted()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntityDelete.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Users.AnyAsync(u => u.Id == UserSeeds.UserEntityDelete.Id));
    }

    [Fact]
    public async Task Delete_UserWithProjectActivity_Throws()
    {
        //Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity2.Id));
    }

    [Fact]
    public async Task NewUser_InsertOrUpdate_UserAdded()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = "Jane",
            Surname = "Doe",
            ImageUrl = null,
            Projects = new ObservableCollection<UserProjectListModel>(),
            Activities = new ObservableCollection<UserProjectActivityListModel>()
        };

        //Act
        user = await _userFacadeSUT.SaveAsync(user);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var userFromDb = await dbxAssert.Users.SingleAsync(u => u.Id == user.Id);
        DeepAssert.Equal(user, UserModelMapper.MapToDetailModel(userFromDb));
    }
}