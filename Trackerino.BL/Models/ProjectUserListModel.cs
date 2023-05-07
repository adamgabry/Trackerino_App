namespace Trackerino.BL.Models
{
    public record ProjectUserDetailModel:ModelBase
    {
        public required Guid UserId { get; set; }
        public required string UserName { get; set; }

        public required string UserSurname { get; set; }
        public required string UserImageUrl { get; set; }

        public static ProjectUserDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Empty,
            UserName = string.Empty,
            UserSurname = string.Empty,
            UserImageUrl = string.Empty

        };
    }
}