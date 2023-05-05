using System.ComponentModel.DataAnnotations;

namespace Shared.Model;

public class Post
{
    public string post  { get; set; }
    public string title { get; set; }
    public User user  { get; set; }
    [Key] 
    public int Id  { get; set; }

    
    public Post(User user, string title, string post )
    {
        this.title = title;
        this.post = post;
        this.user = user;
    }
    
    private Post(){}
}