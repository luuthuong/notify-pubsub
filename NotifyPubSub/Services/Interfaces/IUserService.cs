namespace VNG.SocialNotify.Services.Interfaces;

public interface IUserService
{
    UserModel? GetUser(int userId);
    void Add(UserModel user);
    void Add(IEnumerable<UserModel> users);
    IEnumerable<UserModel> GetListFollower(int userId);
}
