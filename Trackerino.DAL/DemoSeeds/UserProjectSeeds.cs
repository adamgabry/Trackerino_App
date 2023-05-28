using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL.DemoSeeds;
public static class UserProjectSeeds
{
    public static readonly UserProjectEntity UserProjectMax = new()
    {
        Id = Guid.Parse(input: "58F19566-686F-4093-A8A6-77B20EA10863"),
        UserId = UserSeeds.Max.Id,
        ProjectId = ProjectSeeds.ProjectEntityICS.Id,
    };
    public static readonly UserProjectEntity UserProjectMax1 = new()
    {
        Id = Guid.Parse(input: "86584ff0-b5c3-43a5-9f37-cb4369eafec6"),
        UserId = UserSeeds.Max.Id,
        ProjectId = ProjectSeeds.ProjectEntityGen.Id,
    };

    public static readonly UserProjectEntity UserProjectSeidly = new()
    {
        Id = Guid.Parse(input: "A2E6849D-A158-4436-980C-7FC26B60C674"),
        UserId = UserSeeds.Seidly.Id,
        ProjectId = ProjectSeeds.ProjectEntityICS.Id,
    };
    public static readonly UserProjectEntity UserProjectSeidly1 = new()
    {
        Id = Guid.Parse(input: "92db8095-183c-4b50-95ca-5b74f1d44a2f"),
        UserId = UserSeeds.Seidly.Id,
        ProjectId = ProjectSeeds.ProjectEntityStudy.Id,
    };

    public static readonly UserProjectEntity UserProjectAdamkO = new()
    {
        Id = Guid.Parse(input: "30872EFF-CED4-4F2B-89DB-0EE83A74D279"),
        UserId = UserSeeds.AdamkO.Id,
        ProjectId = ProjectSeeds.ProjectEntityGen.Id,
    };

    public static readonly UserProjectEntity UserProjectYeet = new()
    {
        Id = Guid.Parse(input: "e3800a55-4f4f-4a22-9685-a3696b2be67d"),
        UserId = UserSeeds.Yeet.Id,
        ProjectId = ProjectSeeds.ProjectEntityICS.Id,
    };

    public static readonly UserProjectEntity UserProjectCervenyPanda = new()
    {
        Id = Guid.Parse(input: "e20e4ca6-3055-47c0-a079-77ae6ff148e8"),
        UserId = UserSeeds.CervenyPanda.Id,
        ProjectId = ProjectSeeds.ProjectEntityGen.Id,
    };

    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProjectEntity>().HasData(
            UserProjectAdamkO,
            UserProjectCervenyPanda,
            UserProjectYeet,
            UserProjectSeidly,
            UserProjectSeidly1,
            UserProjectMax,
            UserProjectMax1
        );
    }
}