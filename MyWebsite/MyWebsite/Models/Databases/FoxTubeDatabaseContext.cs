using Microsoft.EntityFrameworkCore;
using MyWebsite.Areas.API.Models;

namespace MyWebsite.Models.Databases
{
	public class FoxTubeDatabaseContext : DbContext
	{
		public DbSet<MetricsPackage> Metrics { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Changelog> Changelogs { get; set; }

		public FoxTubeDatabaseContext(DbContextOptions<FoxTubeDatabaseContext> options) : base(options) =>
			Database.EnsureCreated();
	}
}