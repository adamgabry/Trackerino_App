namespace Trackerino.BL.Models
{
    public record UserProjectDetailModel : ModelBase
    {
        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public UserListModel? User { get; init; }
        public ProjectListModel? Project { get; init; }
        public Guid UserProjectId { get; set; }
        public static UserProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            UserProjectId = Guid.Empty,
            UserId = Guid.Empty,
            ProjectId = Guid.Empty,
        };
    };
}