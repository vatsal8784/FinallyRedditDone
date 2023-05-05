namespace Shared.DTO;

public class CreatePostDTO
{
    public int UserId { get; }
    public string Title { get; }
    public string Post { get; }

    public CreatePostDTO(int userId, string title, string post)
    {
        UserId = userId;
        Title = title;
        Post = post;
    }
}