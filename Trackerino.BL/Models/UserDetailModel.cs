using Trackerino.DAL.Entities;

namespace Trackerino.BL.Models
{
    public record UserDetailModel : ModelBase
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ActivityListModel>? Activities { get; set; }
        public ICollection<ProjectListModel> Projects { get; set; } = new List<ProjectListModel>();
    }
}