using Shared.Model;

namespace App.DAOinterface;

public interface IUserDAO
{
    Task<User> RegisteruserAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task<User?> GetByIdAsync(object ownerId);
}