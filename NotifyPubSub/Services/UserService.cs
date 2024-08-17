namespace VNG.SocialNotify.Services;

public class UserService : IUserService
{
    private static List<UserModel> _users = [];

    public void Add(UserModel user)
    {
        _users.Add(user);
    }

    public void Add(IEnumerable<UserModel> users){
        _users.AddRange(users);
    }

    public IEnumerable<UserModel> GetListFollower(int userId)
    {
        var user = _users.FirstOrDefault(x => x.Id == userId);
        if (user == null)
            return [];

        return _users.Where(x => user.FollowedUserIds.Contains(x.Id));
    }

    public UserModel? GetUser(int userId)
    {
        return _users.SingleOrDefault(x => x.Id == userId);
    }
}
