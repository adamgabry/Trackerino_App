using Trackerino.DAL.Common;

namespace Trackerino.BL.Models
{
    public record UserProjectActivityListModel : ModelBase
    {
        public required DateTime StartDateTime { get; set; }
        public required DateTime EndDateTime { get; set; }
        public ActivityTag Tag { get; set; }


        public static UserProjectActivityListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            StartDateTime = default,
            EndDateTime = default,
            Tag = ActivityTag.None,
        };
    }
}