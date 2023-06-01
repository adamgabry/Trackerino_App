namespace Trackerino.BL.Models
{
    public record ProjectUserListModel:ModelBase
    {
        public required Guid UserId { get; set; }
        public required string UserName { get; set; }

        public required string UserSurname { get; set; }
        public required string? UserImageUrl { get; set; }

        public static ProjectUserListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Empty,
            UserName = string.Empty,
            UserSurname = string.Empty,
            UserImageUrl = string.Empty

        };
    }
}