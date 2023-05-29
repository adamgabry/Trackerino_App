using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Common;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL.DemoSeeds;

public static class ActivitySeeds
{

    public static readonly ActivityEntity ActivityEntityIOS = new()
    {
        Id = Guid.Parse(input: "4FA608F9-77D2-498B-A6C1-387FDA3DFB3D"),
        Tag = ActivityTag.School,
        Description = "Study session for IOS",
        StartDateTime = new DateTime(2023, 5, 24, 14, 0, 0),
        EndDateTime = new DateTime(2023, 5, 24, 16, 32, 0),
        UserId = UserSeeds.Seidly.Id,
        ProjectId = ProjectSeeds.ProjectEntityStudy.Id
    };

    public static readonly ActivityEntity ActivityEntityDb = new()
    {
        Id = Guid.Parse(input: "143332B9-080E-4953-AEA5-BEF64679B052"),
        Tag = ActivityTag.School,
        Description = "DAL implementation",
        StartDateTime = new DateTime(2023, 4, 8, 15, 28, 0),
        EndDateTime = new DateTime(2023, 4, 8, 18, 41, 0),
        UserId = UserSeeds.Seidly.Id,
        ProjectId = ProjectSeeds.ProjectEntityICS.Id
    };

    public static readonly ActivityEntity ActivityEntityUi = new()
    {
        Id = Guid.Parse(input: "274D0CC9-A948-4818-AADB-A8B4C0506619"),
        Tag = ActivityTag.School,
        Description = "Working on MAUI",
        StartDateTime = new DateTime(2023, 4, 28, 22, 10, 53),
        EndDateTime = new DateTime(2023, 4, 29, 01,  21, 31),
        UserId = UserSeeds.Yeet.Id,
        ProjectId = ProjectSeeds.ProjectEntityICS.Id
    };
    public static readonly ActivityEntity ActivityEntitySport = new()
    {
        Id = Guid.Parse(input: "F7EDB698-5130-4FCB-8F49-C1AC22DACFE8"),
        Tag = ActivityTag.Sport,
        Description = "Canoeing session",
        StartDateTime = new DateTime(2022, 6, 24, 10, 0, 0),
        EndDateTime = new DateTime(2022, 6, 24, 11, 30, 21),
        UserId = UserSeeds.Max.Id,
        ProjectId = ProjectSeeds.ProjectEntityGen.Id
    };

    public static readonly ActivityEntity ActivityEntityModel = new()
    {
        Id = Guid.Parse(input: "d1b26976-f1a9-4398-9cc0-32cbcfa63701"),
        Tag = ActivityTag.School,
        Description = "Implementing models for project",
        StartDateTime = new DateTime(2023, 4, 30, 14, 0, 0),
        EndDateTime = new DateTime(2023, 4, 30, 15, 0, 0),
        UserId = UserSeeds.Max.Id,
        ProjectId = ProjectSeeds.ProjectEntityICS.Id
    };
    public static readonly ActivityEntity ActivityEntityGame = new()
    {
        Id = Guid.Parse(input: "b84823ae-bfbe-4068-8ed6-2980eed4ef07"),
        Tag = ActivityTag.Fun,
        Description = "HC Gaming",
        StartDateTime = new DateTime(2023, 1, 24, 14, 0, 0),
        EndDateTime = new DateTime(2023, 1, 24, 15, 0, 0),
        UserId = UserSeeds.AdamkO.Id,
        ProjectId = ProjectSeeds.ProjectEntityGen.Id
    };

    public static readonly ActivityEntity ActivityEntityNone = new()
    {
        Id = Guid.Parse(input: "26e0bf93-f9e4-4e46-a38f-0f3cd3973e84"),
        Tag = ActivityTag.None,
        Description = "Bing Chilling",
        StartDateTime = new DateTime(2023, 1, 24, 14, 0, 0),
        EndDateTime = new DateTime(2023, 1, 24, 15, 0, 0),
        UserId = UserSeeds.CervenyPanda.Id,
        ProjectId = ProjectSeeds.ProjectEntityGen.Id,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntityNone,
            ActivityEntityDb,
            ActivityEntityGame,
            ActivityEntityIOS,
            ActivityEntityModel,
            ActivityEntitySport,
            ActivityEntityUi
            );
    }
}

