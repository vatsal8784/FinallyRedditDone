using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace EfcDataAccess;

public class PostContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Reddit.db");
    }
}