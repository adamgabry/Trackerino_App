using Trackerino.DAL.Common;

namespace Trackerino.DAL.Entities;

public record ActivityEntity : IEntity
{
    public required Guid Id { get; set; }
    public required DateTime StartDateTime { get; set; }
    public required DateTime EndDateTime { get; set; }
    public ActivityTag Tag { get; set; }
    public string? Description { get; set; }
    public UserEntity? User { get; set; }
    public required Guid UserId { get; set; }
    public ProjectEntity? Project { get; set; }
    public  required  Guid ProjectId { get; set; }
}