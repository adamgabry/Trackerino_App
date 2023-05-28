using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity EmptyUserEntity = new()
    {
        Id = default,
        Name = default!,
        Surname = default!,
        ImageUrl = default
    };
    
    public static readonly UserEntity UserEntity = new()
    {
        Id = Guid.Parse("C6128DE8-A1A1-45FC-A777-4E6CF056EBB0"),
        Name = "Eliot",
        Surname = "Anderson",
        ImageUrl = "https://static.wikia.nocookie.net/mrrobot/images/3/3e/Elliot.jpg/revision/latest?cb=20150810201239"
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity UserEntityWithNoActivities = UserEntity with { Id = Guid.Parse("E1A7B31F-0223-4B1B-B8AD-4AAA627908B9"), Projects = Array.Empty<UserProjectEntity>(), Activities = Array.Empty<ActivityEntity>() };
    public static readonly UserEntity UserEntityWithNoProjects = UserEntity with { Id = Guid.Parse("A4EAD4BA-E1EC-43D9-B6A8-38DD8EF70BF2"), Projects = Array.Empty<UserProjectEntity>(), Activities = Array.Empty<ActivityEntity>() };
    public static readonly UserEntity UserEntityUpdate = UserEntity with { Id = Guid.Parse("77146EA0-2D86-4874-B75E-FBA628AFC698") };
    public static readonly UserEntity UserEntityDelete = UserEntity with { Id = Guid.Parse("88DB5C3B-BD8D-439A-BA86-B7DD75735185") };

    public static UserEntity UserEntity1 = new()
    {
        Id = Guid.Parse("3FADA1AF-3777-43B8-817B-B796005BDC43"),
        Name = "User1 seed Name ",
        Surname = "User1 seed Surname",
        ImageUrl = null
    };

    public static UserEntity UserEntity2 = new()
    {
        Id = Guid.Parse("B86D6D89-E319-44CA-A5B5-03F3B61C6079"),
        Name = "User2 seed Name ",
        Surname = "User2 seed Surname",
        ImageUrl = null
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntityWithNoActivities,
            UserEntityWithNoProjects,
            UserEntity1,
            UserEntity2,
            UserEntity,
            UserEntityUpdate,
            UserEntityDelete);
    }
}

