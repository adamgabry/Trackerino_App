namespace Trackerino.DAL.Entities;

public class ProjectEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<ActivityEntity>? Activities { get; set; }
    public required ICollection<UserEntity> Users { get; set; }
}