using BarberShopApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShopApi.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> opt) : DbContext(opt)
    {
        public DbSet<BarberShop> BarberShops { get; set; }
        public DbSet<Barber> Barbers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BarberShop>().HasMany(b => b.Barbers).WithOne(b => b.BarberShop).HasForeignKey(b => b.BarberShopId);  
            base.OnModelCreating(modelBuilder);
        }
    }
}
