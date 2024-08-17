using System;

namespace VNG.SocialNotify.Test;

public class DummyUserService : IUserService
{
    public void Add(UserModel user)
    {
        throw new NotImplementedException();
    }

    public void Add(IEnumerable<UserModel> users)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserModel> GetListFollower(int userId)
    {
        throw new NotImplementedException();
    }

    public UserModel? GetUser(int userId)
    {
        throw new NotImplementedException();
    }
}
