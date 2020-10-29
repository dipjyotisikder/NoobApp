
using Microsoft.EntityFrameworkCore;

namespace NoobApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


    }

}