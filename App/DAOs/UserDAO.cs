using App.DAOinterface;
using FileData;
using Shared.Model;

namespace App.DAOs;

public class UserDAO : IUserDAO
{
    private readonly FileContext context;

    public UserDAO(FileContext context)
    {
        this.context = context;
    }

    public Task<User> RegisteruserAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(object ownerId)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Id == (int)ownerId);
        return Task.FromResult(existing);
    }
}