using equipmentOrderingService.Models;
using Microsoft.EntityFrameworkCore;

namespace equipmentOrderingService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<IndustrialPremises> IndustrialPremises { get; set;}
        public DbSet<TechnicalEquipment> TechnicalEquipment { get; set;}
    }
}
