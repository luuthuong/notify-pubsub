namespace VNG.SocialNotify.Services.Interfaces;

public interface IPostService
{
    void Add(PostModel postModel);
    void Add(IEnumerable<PostModel> postModels);
    PostModel? GetPostById(int id);
    IEnumerable<PostModel> GetListPost();
    IEnumerable<PostModel> GetListPostByUserId(int userId);

    void Publish(int id);
}
