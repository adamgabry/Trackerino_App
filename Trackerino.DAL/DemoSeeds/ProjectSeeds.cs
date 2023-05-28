using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL.DemoSeeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity ProjectEntityICS = new()
    {
        Id = Guid.Parse(input: "0D7D53AE-D631-4DAA-8C71-C3370E69A16B"),
        Name = "ICS C# Project",
    };
    public static readonly ProjectEntity ProjectEntityStudy = new()
    {
        Id = Guid.Parse(input: "229EC7A1-58BF-4269-8A82-EC741A5ABFAC"),
        Name = "Studies for exams",
    };
    public static readonly ProjectEntity ProjectEntityDev = new()
    {
        Id = Guid.Parse(input: "98B7F7B6-0F51-43B3-B8C0-B5FCFFF6DC2E"),
        Name = "Game Development",
    };
    public static readonly ProjectEntity ProjectEntityGen = new()
    {
        Id = Guid.Parse(input: "1ee3be2a-d899-4d51-b6c7-6bea482957d5"),
        Name = "Others",
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntityStudy,
            ProjectEntityDev,
            ProjectEntityICS,
            ProjectEntityGen
        );
    }
}