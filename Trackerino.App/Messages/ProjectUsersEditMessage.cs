namespace Trackerino.App.Messages
{
    public record ProjectUsersEditMessage()
    {
        public required Guid UserId { get; init; }
        public required Guid ProjectId { get; init; }
    }
}