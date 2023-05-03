using System.Collections.ObjectModel;
using Trackerino.DAL.Common;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Models
{
    public record UserDetailModel : ModelBase
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public ObservableCollection<ActivityListModel>? Activities { get; set; } = new();
        public ObservableCollection<UserProjectListModel> Projects { get; set; } = new();

        public static UserDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Surname = string.Empty,
        };
    }
}