using App.DAOinterface;
using App.DAOs;
using App.LogicInterface;
using Shared.DTO;
using Shared.Model;

namespace App.Logic;

public class PostLogic : IPostLogic
{
    
    private readonly IPostDAO postDao;
    private readonly IUserDAO userDao;

    public PostLogic(IPostDAO postDao, IUserDAO userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    public async Task<Post> CreatePostAsync(CreatePostDTO createPostDto)
    {
        User? user = await userDao.GetByIdAsync(createPostDto.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {createPostDto.UserId} was not found.");
        }

        ValidatePost(createPostDto);
        Post post = new Post(user, createPostDto.Title, createPostDto.Post );
        Post created = await postDao.CreatePostAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetPostAsync(SearchPostDTO searchPostDto)
    {
        return postDao.GetPostAsync(searchPostDto);
    }

    public Task<IEnumerable<Post>> GetAllPosts()
    {
        return postDao.GetAllPosts();
    }
    
    private void ValidatePost(CreatePostDTO createPostDto)
    {
        if (string.IsNullOrEmpty(createPostDto.Title)) throw new Exception("Title cannot be empty.");
       
    }
}