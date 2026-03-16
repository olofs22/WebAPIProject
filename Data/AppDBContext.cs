using Microsoft.EntityFrameworkCore;
using WebAPIProject.Models;
namespace WebAPIProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todo { get; set; } //DbSet for Tournament model
        public AppDbContext(DbContextOptions<AppDbContext> options) //constructor for dependency injection
            : base(options)
        {

        }
    }
}
