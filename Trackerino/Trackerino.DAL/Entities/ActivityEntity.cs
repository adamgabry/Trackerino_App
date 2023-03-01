namespace Trackerino.DAL.Entities;
public class ActivityEntity : IEntity
{
    public required Guid Id { get; set; }
    public required DateTime StarDateTime { get; set; }
    public required DateTime EndDateTime { get; set; }
    public Enum Tag { get; set; }
    public string? Description { get; set; }
}