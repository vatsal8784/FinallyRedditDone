using System.Net.Http.Json;
using System.Text.Json;
using Shared.DTO;
using Shared.Model;

namespace BlazorApp.Services.Http;

public class PostService : IPostService
{
    
    private readonly HttpClient client;

    public PostService(HttpClient client)
    {
        this.client = client;
    }
    public async Task CreatePostAsync(CreatePostDTO createPostDto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/post", createPostDto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Post>> GetPostAsync(string? username, int? userId, string? titleContains, int? postId)
    {
        HttpResponseMessage response = await client.GetAsync("/postdetails");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }
}