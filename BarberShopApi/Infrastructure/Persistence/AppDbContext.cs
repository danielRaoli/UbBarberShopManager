using BarberShopApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarberShopApi.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> opt) : IdentityDbContext<User,IdentityRole<Guid>, Guid>(opt)
    {
        public DbSet<BarberShop> BarberShops { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BarberShop>().HasMany(b => b.Barbers).WithOne(b => b.BarberShop).HasForeignKey(b => b.BarberShopId);

            modelBuilder.Entity<Barber>().HasMany(b => b.Services).WithOne(s => s.Baber).HasForeignKey(s => s.BarberId);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Agendamento>()
                        .HasOne(a => a.Barber)
                        .WithMany()
                        .HasForeignKey(a => a.BarberId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Agendamento>()
                        .HasOne(a => a.Service)
                        .WithMany()
                        .HasForeignKey(a => a.ServiceId)
                        .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
