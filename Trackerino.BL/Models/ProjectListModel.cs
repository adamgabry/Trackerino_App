using System.Collections.ObjectModel;

namespace Trackerino.BL.Models
{
    public record ProjectListModel : ModelBase
    {
        public required Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public ObservableCollection<UserProjectListModel>? Users { get; set; } = new();
        public static ProjectListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectId = Guid.Empty,
            Name = string.Empty
        };
    }
}