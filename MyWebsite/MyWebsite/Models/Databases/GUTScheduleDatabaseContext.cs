using Microsoft.EntityFrameworkCore;

namespace MyWebsite.Models.Databases
{
	public class GUTScheduleDatabaseContext : DbContext
	{
		public DbSet<ResumeModel> PrivacyPolicies { get; set; }
		public DbSet<CustomData> OffsetDates { get; set; }

		public GUTScheduleDatabaseContext(DbContextOptions<GUTScheduleDatabaseContext> options) : base(options) =>
			Database.EnsureCreated();
	}
}