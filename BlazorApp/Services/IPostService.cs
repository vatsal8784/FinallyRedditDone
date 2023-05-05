using Shared.DTO;
using Shared.Model;

namespace BlazorApp.Services;

public interface IPostService
{
    Task CreatePostAsync(CreatePostDTO createPostDto);
    Task<ICollection<Post>> GetPostAsync(
        string? username, int? userId, string? titleContains,int? postId
    );
}