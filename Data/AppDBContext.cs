using Microsoft.EntityFrameworkCore;
using WebAPIProject.Models;
namespace WebAPIProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tournaments> tournaments { get; set; }
        public DbSet<Game> games { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournaments>().HasKey(t => t.Id);
            modelBuilder.Entity<Game>().HasKey(g => g.Id);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Tournament)
                .WithMany(t => t.Games)
                .HasForeignKey(g => g.TournamentId);
        }
    }
}
