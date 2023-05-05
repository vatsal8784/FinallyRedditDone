using Shared.DTO;
using Shared.Model;

namespace App.LogicInterface;

public interface IPostLogic
{
    Task<Post> CreatePostAsync(CreatePostDTO createPostDto);
    Task<IEnumerable<Post>> GetPostAsync(SearchPostDTO searchPostDto);
    Task<IEnumerable<Post>> GetAllPosts();
}