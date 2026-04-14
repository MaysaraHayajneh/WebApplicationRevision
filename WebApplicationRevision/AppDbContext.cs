using Microsoft.EntityFrameworkCore;
using WebApplicationRevision.Models;

namespace WebApplicationRevision
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>();
			base.OnModelCreating(modelBuilder);

		}

	}
}
