using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL.DemoSeeds;

public static class UserSeeds
{
    public static readonly UserEntity Max = new()
    {
        Id = Guid.Parse("f49c20ea-154c-4cde-91ac-8d117b6ee66d"),
        Name = "Maxmilián",
        Surname = "Nový",
        ImageUrl = "https://static.wikia.nocookie.net/mrrobot/images/3/3e/Elliot.jpg/revision/latest?cb=20150810201239"
    };

    public static readonly UserEntity Seidly = new()
    {
        Id = Guid.Parse("2b83ecce-91e0-45eb-957b-c189773994b0"),
        Name = "Ondřej",
        Surname = "Seidl",
        ImageUrl = "https://static.wikia.nocookie.net/mrrobot/images/4/40/Tumblr_9e63f38c6b4ab8b60b7af43ad8a40584_b5200ced_640.jpg/revision/latest/scale-to-width-down/350?cb=20190928213859"
    };
    public static readonly UserEntity Yeet = new()
    {
        Id = Guid.Parse("6b3c8391-0774-47b5-ab88-a50148aabd7f"),
        Name = "Matyáš",
        Surname = "Strelec",
        ImageUrl = "https://static.wikia.nocookie.net/mrrobot/images/8/87/Gideon_001.jpg/revision/latest/scale-to-width-down/350?cb=20150607154334"
    };
    public static readonly UserEntity AdamkO = new()
    {
        Id = Guid.Parse("0551627e-349c-4980-a3ea-6f71c395eb23"),
        Name = "Adam",
        Surname = "Gabrys",
        ImageUrl = "https://static.wikia.nocookie.net/mrrobot/images/8/86/EdwardAlderson.jpg/revision/latest/scale-to-width-down/350?cb=20161117005643"
    };

    public static readonly UserEntity CervenyPanda = new()
    {
        Id = Guid.Parse("f1fc25f9-60ca-4a79-9907-6c27e23dc869"),
        Name = "Jakub",
        Surname = "Kovář",
        ImageUrl = "https://static.wikia.nocookie.net/mrrobot/images/1/1a/Mr.-Robot-1x04-3.jpg/revision/latest/scale-to-width-down/350?cb=20150725100044",
        Projects = new List<UserProjectEntity>(),
        Activities = new List<ActivityEntity>()
    };

    // static UserSeeds()
    // {
    //     AdamkO.Activities.Add(ActivitySeeds.ActivityEntityGame);
    //     Seidly.Activities.Add(ActivitySeeds.ActivityEntityIOS);
    //     Seidly.Activities.Add(ActivitySeeds.ActivityEntityDb);
    //     Yeet.Activities.Add(ActivitySeeds.ActivityEntityUi);
    //     Max.Activities.Add(ActivitySeeds.ActivityEntitySport);
    //     Max.Activities.Add(ActivitySeeds.ActivityEntityModel);
    //     CervenyPanda.Activities.Add(ActivitySeeds.ActivityEntityNone);
    // }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            CervenyPanda,
            AdamkO,
            Yeet,
            Seidly,
            Max
            );
    }
}

