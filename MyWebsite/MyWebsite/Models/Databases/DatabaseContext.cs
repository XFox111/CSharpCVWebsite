using Microsoft.EntityFrameworkCore;

namespace MyWebsite.Models.Databases
{
	public class DatabaseContext : DbContext
	{
		public DbSet<LinkModel> Links { get; set; }
		public DbSet<ImageModel> Gallery { get; set; }
		public DbSet<ProjectModel> Projects { get; set; }
		public DbSet<CredentialModel> Users { get; set; }
		public DbSet<BadgeModel> Badges { get; set; }
		public DbSet<ResumeModel> Resume { get; set; }
		public DbSet<ShortLinkModel> ShortLinks { get; set; }
		public DbSet<CustomData> CustomData { get; set; }

		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) =>
			Database.EnsureCreated();
	}
}