using equipmentOrderingService.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace equipmentOrderingService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<IndustrialPremises> IndustrialPremises { get; set;}
        public DbSet<TechnicalEquipment> TechnicalEquipment { get; set;}
        public DbSet<OrderingContract> OrderingContracts { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderingContract>()
                .HasOne(oc => oc.Premises)
                .WithMany()
                .IsRequired();
            modelBuilder.Entity<OrderingContract>()
                .HasOne(oc=>oc.EquipmentType)
                .WithMany()
                .IsRequired();
        }

    }
}
