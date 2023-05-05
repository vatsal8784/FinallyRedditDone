using App.LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Model;

namespace WebAPI.Controllers;
[ApiController] //marks this class as a Web API controller, so that the Web API framework will know about it
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePostAsync([FromBody] CreatePostDTO createPostDto)
    {
        try
        {
            Post created = await postLogic.CreatePostAsync(createPostDto);
            return Created($"/posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet,Route("/postdetails")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPostAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] string? titleContains, [FromQuery] int? Id)
    {
        try
        {
            SearchPostDTO searchPostDto = new(userName, userId, titleContains,Id);
            var posts = await postLogic.GetPostAsync(searchPostDto);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}