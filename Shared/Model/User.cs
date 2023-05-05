using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Model;

public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password{ get; set; }
    
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; }

    
   
}