using System.Security.Claims;
using Shared.DTO;
using Shared.Model;

namespace BlazorApp.Services;

public interface IUserService
{
    Task<User> RegisterUserAsync(RegisterUserDTO registerUserDto);
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    
    //used to provide authentication state details to the Blazor auth framework, whenever the app needs to know about who is logged in
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}