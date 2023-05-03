using App.DAOinterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTO;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class PostEfcDAO : IPostDAO
{
    private readonly PostContext vPostContext;

    public PostEfcDAO(PostContext context)
    {
        this.vPostContext = context;
    }
    public async Task<Post> CreatePostAsync(Post post)
    {
        EntityEntry<Post> added = await vPostContext.Posts.AddAsync(post);
        await vPostContext.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetPostAsync(SearchPostDTO searchPostDto)
    {
        IQueryable<Post> query = vPostContext.Posts.Include(post => post.user).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchPostDto.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                post.user.UserName.ToLower().Equals(searchPostDto.Username.ToLower()));
        }
    
        if (searchPostDto.UserId != null)
        {
            query = query.Where(t => t.user.Id == searchPostDto.UserId);
        }
        
    
        if (!string.IsNullOrEmpty(searchPostDto.TitleContains))
        {
            query = query.Where(t =>
                t.title.ToLower().Contains(searchPostDto.TitleContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        var result =  vPostContext.Posts.AsEnumerable();
        result = vPostContext.Posts;
        return result;
    }
}