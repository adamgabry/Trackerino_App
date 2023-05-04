namespace Trackerino.BL.Models
{
    public record UserListModel : ModelBase
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public static UserListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Empty,
            Name = string.Empty,
            Surname = string.Empty,
        };
    }
}