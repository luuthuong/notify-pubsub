# Notify Pubsub
This project aims to demonstrate the workflow of a publish-subscribe (pub-sub) service in a social media-like application. It showcases how events can be published when a user posts content, and how subscribers (followers) can be notified of these events.

The pub-sub pattern allows for loose coupling between components, where publishers (in this case, users posting content) don't need to know about their subscribers (followers). This design promotes scalability and flexibility in event-driven architectures.

Key features demonstrated:
- Publishing posts and triggering events
- Managing user subscriptions (following/followers)

This simple implementation provides a foundation for understanding how larger-scale pub-sub systems might function in real-world applications.

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
Here's the code snippet for the `Publish` method in `PostService`:

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