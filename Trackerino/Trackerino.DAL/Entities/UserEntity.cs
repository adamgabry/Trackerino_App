namespace Trackerino.DAL.Entities;

public class UserEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<ActivityEntity>? Activities { get; set; }
    public required ICollection<ProjectEntity> Projects { get; set; }

}