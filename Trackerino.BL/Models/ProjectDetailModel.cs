using Trackerino.DAL.Entities;

namespace Trackerino.BL.Models
{
    public record ProjectDetailModel : ModelBase
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ActivityListModel>? Activities { get; set; }
        public required ICollection<UserListModel> Users { get; set; } = new List<UserListModel>();
    }
}