namespace Trackerino.BL.Models
{
    public record UserProjectDetailModel : ModelBase
    {
        public required Guid ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public static UserProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectName = string.Empty,
            ProjectId = Guid.Empty,
        };
    };
}