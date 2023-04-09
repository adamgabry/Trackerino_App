using System.Collections.ObjectModel;
using Trackerino.DAL.Common;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Models
{
    public record ProjectDetailModel : ModelBase
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public ObservableCollection<ActivityListModel>? Activities { get; set; }
        public ObservableCollection<UserProjectListModel>? Users { get; set; } = new();

        public static ProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };
    }
}