
# VNG.SocialNotify

## Structure

------------
Components:

* **PostService**: responsible for managing posts and publishing events to followers.
* **UserService**: responsible for managing user data and subscriptions.
* **EventPublisher**: responsible for publishing events to subscribers.

## How to Run the Project

-------------------------

To run the project, follow these steps:

### Steps

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio Code or Visual Studio 2019.
3. Build the solution by running `dotnet build` in the terminal.
4. Run the project by clicking on the "Run" button or by running `dotnet run` in the terminal.
5. Run the tests cases via CLI `dotnet test`.
6. Observe the event being published to the **EventPublisher** and received by subscribers Eg: `NotifyToFollowerSubcriber`.


## Sample flow of the publish post function
Sure, I can help you with that. The `Publish` method in the `PostService` class is responsible for publishing a post and sending notifications to followers. Here's a breakdown of the flow of the `Publish` method:

1. It first retrieves the post by calling the `GetPostById` method with the provided `id`.
2. If the post is not found, it throws an exception indicating that the post was not found.
3. If the post is already published, it throws an exception indicating that the post is already published.
4. If the post exists and is not published, it sets the `IsPublished` property of the post to `true`.
5. It retrieves the author of the post by calling the `GetUser` method of the `userService`.
6. It retrieves the list of followers by calling the `GetListFollower` method of the `userService`.
7. It creates a list of notifications by selecting each follower and creating a `NotifyToFollower` object with the follower's ID and a message indicating that the author has published a new post.
8. It publishes each notification by calling the `Publish` method of the `eventPublisher` to trigger the subscribers to receive the notifications.

Here's the code snippet for the `Publish` method:

```csharp
public void Publish(int id)
{
    var post = GetPostById(id);
    if (post == null)
        throw new Exception("Post not found");
    if (post.IsPublished)
        throw new Exception("Post already published");

    post.IsPublished = true;

    var author = userService.GetUser(post.UserId);

    var followers = userService.GetListFollower(post.UserId);

    var notifications = followers.Select(x => new NotifyToFollower()
    {
        UserFollowerId = x.Id,
        Message = $"Author {author?.Name} published a new post"
    });

    foreach (var notification in notifications)
    {
        eventPublisher.Publish(notification);
    }
}
```