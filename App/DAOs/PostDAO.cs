using App.DAOinterface;
using FileData;
using Shared.DTO;
using Shared.Model;

namespace App.DAOs;

public class PostDAO : IPostDAO
{
    private readonly FileContext context;
    
    public PostDAO(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreatePostAsync(Post post)
    {
        int postId = 1;
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(p => p.Id);
            postId++;
        }

        post.Id = postId;

        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetPostAsync(SearchPostDTO searchPostDto)
    {
        var result =  context.Posts.AsEnumerable();
        
        if (!string.IsNullOrEmpty(searchPostDto.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.Posts.Where(p=>
                p.user.UserName.Equals(searchPostDto.Username, StringComparison.OrdinalIgnoreCase));
        }
        
        
        if (searchPostDto.UserId != null)
        {
            result = result.Where(p => p.user.Id == searchPostDto.UserId);
        } 
        
        if (searchPostDto.PostId != null)
        {
            result = result.Where(p => p.Id == searchPostDto.PostId);
        }
        
        if (!string.IsNullOrEmpty(searchPostDto.TitleContains))
        {
            result = result.Where(p =>
                p.post.Contains(searchPostDto.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        var result =  context.Posts.AsEnumerable();
        result = context.Posts;
        return result;
    }

    
    
    
}