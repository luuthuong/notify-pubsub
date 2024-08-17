namespace VNG.SocialNotify.Domain;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> FollowedUserIds { get; set; } = new List<int>();
}