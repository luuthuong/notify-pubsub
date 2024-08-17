namespace VNG.SocialNotify.Tests;

public class PostServiceTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IEventPublisher> _mockEventPublisher;
    private readonly PostService _postService;

    public PostServiceTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockEventPublisher = new Mock<IEventPublisher>();
        _postService = new PostService(_mockUserService.Object, _mockEventPublisher.Object);
    }

    [Fact]
    public void Publish_PostDoesNotExist_ThrowException()
    {
        // Act and Assert
        Assert.Throws<Exception>(() => _postService.Publish(1));
    }

    [Fact]
    public void Publish_PostAlreadyPublished_ThrowException()
    {
        // Arrange
        var post = new PostModel { Id = 1, UserId = 1, IsPublished = true };
        _postService.Add(post);

        // Act and Assert
        Assert.Throws<Exception>(() => _postService.Publish(post.Id));
    }

    [Fact]
    public void Publish_PostExistsAndNotPublished_PublishPostAndSendNotifications()
    {
        // Arrange
        var post = new PostModel { Id = 1, UserId = 1, IsPublished = false };
        var user = new UserModel { Id = 1, Name = "Test User" };
        var followers = new List<UserModel> { new UserModel { Id = 2, Name = "Follower 1" } };

        _mockUserService.Setup(u => u.GetUser(post.UserId)).Returns(user);
        _mockUserService.Setup(u => u.GetListFollower(post.UserId)).Returns(followers);
        _postService.Add(post);

        // Act
        _postService.Publish(post.Id);

        // Assert
        Assert.True(post.IsPublished);
        _mockEventPublisher.Verify(e => e.Publish(It.IsAny<NotifyToFollower>()), Times.Once);
        _mockUserService.Verify(e => e.GetUser(It.IsAny<int>()), Times.Once);
    }


    [Fact]
    public void Publish_PublishOnlyFollowers_ReceiveNotifications()
    {
        // Arrange
        PostModel post = new() { Id = 1, UserId = 1, IsPublished = false };
        UserModel user = new() { Id = 1, Name = "Test User" };

        IList<UserModel> followers = [
            new UserModel { Id = 1, Name = "Follower 1" },
            new UserModel { Id = 2, Name = "Follower 2" },
        ];

        UserModel notFollower = new() { Id = 3, Name = "Not Follower" };

        _mockUserService.Setup(u => u.GetUser(post.UserId)).Returns(user);
        _mockUserService.Setup(u => u.GetListFollower(post.UserId)).Returns(followers);

        _postService.Add(post);

        // Act
        _postService.Publish(post.Id);

        // Assert
        Assert.True(post.IsPublished);
        _mockEventPublisher.Verify(e => e.Publish(It.Is<NotifyToFollower>(x => followers.Any(f => f.Id == x.UserFollowerId))), Times.Exactly(followers.Count));
        _mockEventPublisher.Verify(e => e.Publish(It.Is<NotifyToFollower>(x => notFollower.Id == x.UserFollowerId)), Times.Never);
        _mockUserService.Verify(e => e.GetUser(notFollower.Id), Times.Never);
    }

}