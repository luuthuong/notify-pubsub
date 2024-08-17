using System;

namespace VNG.SocialNotify.Test;

public class DummyPostService : IPostService
{
    public void Add(PostModel postModel)
    {
        throw new NotImplementedException();
    }

    public void Add(IEnumerable<PostModel> postModels)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PostModel> GetListPost()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PostModel> GetListPostByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public PostModel? GetPostById(int id)
    {
        throw new NotImplementedException();
    }

    public void Publish(int id)
    {
        throw new NotImplementedException();
    }
}
