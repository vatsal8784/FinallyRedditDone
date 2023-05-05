namespace Shared.DTO;

public class SearchPostDTO
{
    public string? Username  { get;  }
    public int? UserId  { get; }
    public string? TitleContains  { get; }
    public int? PostId { get; }

    public SearchPostDTO(string? username, int? userId, string? titleContains,int? postId)
    {
        Username = username;
        UserId = userId;
        TitleContains = titleContains;
        PostId = postId;
    }
}