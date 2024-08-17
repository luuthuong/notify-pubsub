using System;

namespace VNG.SocialNotify.Events;

public class NotifyToFollowerSubcriber(IUserService userService) : IEventSubcriber<NotifyToFollower>
{
    public void Subscribe(NotifyToFollower @event)
    {
        var user = userService.GetUser(@event.UserFollowerId);
        if(user is null)
            return;
        Console.WriteLine($"Follower: {user.Name} received a notification \t Content: {@event.Message}");
    }
}
