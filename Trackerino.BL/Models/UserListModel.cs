namespace Trackerino.BL.Models
{
    public record UserListModel : ModelBase
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public static UserDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Surname = string.Empty,
        };
    }
}