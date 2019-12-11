using Microsoft.EntityFrameworkCore;

namespace MyWebsite.Areas.API.Models
{
    public class FoxTubeDatabaseContext : DbContext
    {
        public DbSet<MetricsPackage> Metrics { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Changelog> Changelogs { get; set; }

        public FoxTubeDatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
