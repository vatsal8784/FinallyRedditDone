using App.DAOinterface;
using App.DAOs;
using App.LogicInterface;
using Shared.DTO;
using Shared.Model;

namespace App.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO userDao;

    public UserLogic(IUserDAO userDao)
    {
        this.userDao = userDao;
    }
    public async Task<User> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        User? existing = await userDao.GetByUsernameAsync(registerUserDto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(registerUserDto);
        User toCreate = new User
        {
            UserName = registerUserDto.UserName,
            Password = registerUserDto.Password
        };
    
        User created = await userDao.RegisteruserAsync(toCreate);
    
        return created;
    }

    public async Task<User> ValidateUser(RegisterUserDTO registerUserDto)
    {
        User? existingUser = await userDao.GetByUsernameAsync(registerUserDto.UserName);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
    
        if (!existingUser.Password.Equals(registerUserDto.Password))
        {
            throw new Exception("Password mismatch");
        }
    
        return await Task.FromResult(existingUser);
    }
    
    private static void ValidateData(RegisterUserDTO registerUserDto)
    {
        string userName = registerUserDto.UserName;
        string password = registerUserDto.Password;


        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");
        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        if (password.Length < 3)
            throw new Exception("Password must be at least 3 characters!");
    }
}