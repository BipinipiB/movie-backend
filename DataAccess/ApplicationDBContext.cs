using Microsoft.EntityFrameworkCore;

namespace movie_backend.DataAccess
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        // DbSet for Movies or table for Movies
        public DbSet<Models.Movie> Movies { get; set; } = null!;

        // DbSet for Users or Table for Users
        public DbSet<Models.User> Users { get; set; } = null!;

        // DbSet for FavoriteMovies or Table for FavoriteMovies
        public DbSet<Models.FavoriteMovie> FavoriteMovies { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here if needed
        }
    }
}
