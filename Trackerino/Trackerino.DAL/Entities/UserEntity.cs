namespace Trackerino.DAL.Entities;

public class UserEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<ActivityEntity> Activities { get; set; }
    public ICollection<ProjectEntity> Projects { get; set; }

}