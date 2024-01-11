using Front.Areas.Cars.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Front.Areas.Admin.Models
{
    public class ApplicationContext: IdentityDbContext<User>
    {
        public DbSet<Brand> Brands { get; private set; }
        public DbSet<Model> Models { get; private set; }
        public DbSet<Car> Cars { get; private set; }
        public DbSet<Color> Colors { get; private set; }
        public DbSet<Foto> Fotos { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
		{
            //Database.EnsureCreated();
            //Database.EnsureDeleted();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Model>()
        //    .HasOne(o => o.Brand)
        //    .WithOne()
        //    .HasForeignKey<Model>(i => i.Id)
        //    .OnDelete(DeleteBehavior.Cascade);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
