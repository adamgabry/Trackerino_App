using System.Collections.ObjectModel;

namespace Trackerino.BL.Models
{
    public record UserDetailModel : ModelBase
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public ObservableCollection<ActivityListModel>? Activities { get; set; } = new();
        public ObservableCollection<UserProjectListModel> Projects { get; set; } = new();

        public static UserDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Empty,
            Name = string.Empty,
            Surname = string.Empty,
        };
    }
}