namespace VNG.SocialNotify.Events;

public class NotifyToFollower : IEvent
{
    public int UserFollowerId { get; set; }
    public required string Message { get; set; }
}
