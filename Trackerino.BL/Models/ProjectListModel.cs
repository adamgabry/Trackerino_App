using System.Collections.ObjectModel;

namespace Trackerino.BL.Models
{
    public record ProjectListModel : ModelBase
    {
        public required string Name { get; set; }
        public static ProjectListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };
    }
}