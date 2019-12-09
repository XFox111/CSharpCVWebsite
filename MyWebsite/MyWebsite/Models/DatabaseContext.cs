using Microsoft.EntityFrameworkCore;

namespace MyWebsite.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<Image> Gallery { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
