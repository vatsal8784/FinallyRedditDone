using Shared.DTO;
using Shared.Model;

namespace App.DAOinterface;

public interface IPostDAO
{
    Task<Post> CreatePostAsync(Post post);
    Task<IEnumerable<Post>> GetPostAsync(SearchPostDTO searchPostDto);
    Task<IEnumerable<Post>> GetAllPosts();
}