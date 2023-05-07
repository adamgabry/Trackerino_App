namespace Trackerino.BL.Models
{
    public record UserProjectListModel:ModelBase
    {
        public required Guid ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public static UserProjectListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectName = string.Empty,
            ProjectId = Guid.Empty,
        };
    }
}