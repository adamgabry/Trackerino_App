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
        ImageUrl = default
    };

    public static readonly UserEntity Seidly = new()
    {
        Id = Guid.Parse("2b83ecce-91e0-45eb-957b-c189773994b0"),
        Name = "Ondřej",
        Surname = "Seidl",
        ImageUrl = default
    };
    public static readonly UserEntity Yeet = new()
    {
        Id = Guid.Parse("6b3c8391-0774-47b5-ab88-a50148aabd7f"),
        Name = "Matyáš",
        Surname = "Strelec",
        ImageUrl = default
    };
    public static readonly UserEntity AdamkO = new()
    {
        Id = Guid.Parse("0551627e-349c-4980-a3ea-6f71c395eb23"),
        Name = "Adam",
        Surname = "Gabrys",
        ImageUrl = default
    };

    public static readonly UserEntity CervenyPanda = new()
    {
        Id = Guid.Parse("f1fc25f9-60ca-4a79-9907-6c27e23dc869"),
        Name = "Jakub",
        Surname = "Kovář",
        ImageUrl = default
    };

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

