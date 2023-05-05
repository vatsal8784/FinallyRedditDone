using Shared.DTO;
using Shared.Model;

namespace App.LogicInterface;

public interface IUserLogic
{
    Task<User> RegisterUserAsync(RegisterUserDTO registerUserDto);
    Task<User> ValidateUser(RegisterUserDTO registerUserDto);
}