using System.Collections.ObjectModel;
using Trackerino.DAL.Common;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Models
{
    public record ActivityDetailModel : ModelBase
    {
        public required Guid Id { get; set; }
        public required DateTime StartDateTime { get; set; }
        public required DateTime EndDateTime { get; set; }
        public ActivityTag Tag { get; set; }
        public string? Description { get; set; }
        public UserListModel? User { get; set; }
        public ProjectListModel? Project { get; set; }


        public static ActivityDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            StartDateTime = default,
            EndDateTime = default,
            Tag = ActivityTag.None,
        };
    }
}