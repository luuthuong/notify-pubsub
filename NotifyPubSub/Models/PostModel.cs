using System.Text.Json.Serialization;

namespace VNG.SocialNotify.Domain;

public class PostModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    public bool IsPublished { get; set; } = false;
}
