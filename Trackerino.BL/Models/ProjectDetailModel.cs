using System.Collections.ObjectModel;

namespace Trackerino.BL.Models
{
    public record ProjectDetailModel : ModelBase
    {
        public required Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public ObservableCollection<ActivityListModel>? Activities { get; set; } = new ();
        public ObservableCollection<UserProjectListModel> Users { get; set; } = new();

        public static ProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectId = Guid.Empty,
            Name = string.Empty
        };
    }
}