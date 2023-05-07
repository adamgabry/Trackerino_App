namespace Trackerino.DAL.Entities;

public record UserProjectEntity : IEntity
{
    public required Guid UserId { get; set; }
    public required Guid ProjectId { get; set; }
    public UserEntity? User { get; init; }
    public ProjectEntity? Project { get; init; }
    public Guid Id { get; set; }
}