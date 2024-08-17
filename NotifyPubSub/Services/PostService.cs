using System;

namespace VNG.SocialNotify.Services;

public class PostService(
    IUserService userService,
    IEventPublisher eventPublisher
) : IPostService
{
    private readonly List<PostModel> _posts  = [];

    public void Add(PostModel postModel)
    {
        _posts.Add(postModel);
    }

    public void Add(IEnumerable<PostModel> postModels){
        _posts.AddRange(postModels);
    }

    public IEnumerable<PostModel> GetListPost()
    {
        return _posts;
    }

    public IEnumerable<PostModel> GetListPostByUserId(int userId)
    {
        return _posts.Where(x => x.UserId == userId);
    }

    public PostModel? GetPostById(int id)
    {
        return _posts.SingleOrDefault(x => x.Id == id);
    }

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
}
