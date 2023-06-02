using Trackerino.DAL.Common;

namespace Trackerino.BL.Models
{
    public record UserProjectActivityDetailModel : ModelBase
    {
        public required DateTime StartDateTime { get; set; }
        public required DateTime EndDateTime { get; set; }
        public ActivityTag Tag { get; set; }
        public string? Description { get; set; }


        public static UserProjectActivityDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            StartDateTime = default,
            EndDateTime = default,
        };
    }
}