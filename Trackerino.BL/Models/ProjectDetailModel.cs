using System.Collections.ObjectModel;

namespace Trackerino.BL.Models
{
    public record ProjectDetailModel : ModelBase
    {
        public required string Name { get; set; }
        public ObservableCollection<UserProjectActivityListModel> Activities { get; init; } = new ();
        public ObservableCollection<ProjectUserListModel> Users { get; init; } = new();

        public static ProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };
    }
}