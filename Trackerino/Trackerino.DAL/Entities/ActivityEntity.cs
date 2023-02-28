namespace Trackerino.DAL.Entities;
public class ActivityEntity : IEntity
{
    public Guid Id { get; set; }
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public Enum Tag { get; set; }
    public string Description { get; set; }
}