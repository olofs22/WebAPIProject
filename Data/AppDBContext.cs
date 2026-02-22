using Microsoft.EntityFrameworkCore;
using WebAPIProject.Models;
namespace WebAPIProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; } //DbSet for Tournament model
        public DbSet<Game> Games { get; set; } //DbSet for Game model
        protected override void OnModelCreating(ModelBuilder modelBuilder) //configuration for relationships between Tournament and Game models
        {
            modelBuilder.Entity<Tournament>().HasKey(t => t.Id);
            modelBuilder.Entity<Game>().HasKey(g => g.Id);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Tournament)
                .WithMany(t => t.Games)
                .HasForeignKey(g => g.TournamentId);
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) //constructor for dependency injection
            : base(options)
        {

        }
    }
}
