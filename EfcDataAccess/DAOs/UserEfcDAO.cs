using App.DAOinterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class UserEfcDAO : IUserDAO
{
    private readonly PostContext vPostContext;

    public UserEfcDAO(PostContext context)
    {
        this.vPostContext = context;
    }
    public async Task<User> RegisteruserAsync(User? user)
    {
        EntityEntry<User?> newUser = await vPostContext.User.AddAsync(user);
        await vPostContext.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await vPostContext.User.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public async Task<User?> GetByIdAsync(object ownerId)
    {
        User? user = await vPostContext.User.FindAsync(ownerId);
        return user;
    }
}