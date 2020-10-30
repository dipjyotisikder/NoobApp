
using Microsoft.EntityFrameworkCore;
using NoobApp.Models;

namespace NoobApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

    }

}