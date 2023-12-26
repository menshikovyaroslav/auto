using Microsoft.EntityFrameworkCore;

namespace Front.Areas.Admin.Models
{
	public class ApplicationContext: DbContext
	{
		public DbSet<User> Users { get; private set; } = null!;
        public DbSet<Brand> Brands { get; private set; } = null!;
        public DbSet<Model> Models { get; private set; } = null!;
        public DbSet<Car> Cars { get; private set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
		{
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>()
            .HasOne(o => o.Brand)
            .WithOne()
            .HasForeignKey<Model>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
