using System.Collections.ObjectModel;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Models
{
    public record ProjectListModel : ModelBase
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public ObservableCollection<UserProjectListModel>? Users { get; set; } = new();
        public static ProjectListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };
    }
}