using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace SpCollectorsAdminApi.Data
{
 
        public class AppDbContext(DbContextOptions options) : DbContext(options)
        {
        public DbSet<CollectorEntry> CollectorEntry { get; set; }
        public DbSet<PlanDetail> PlanDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CollectorEntry>()
                .HasMany(c => c.Entries)
                .WithOne(p => p.CollectorEntry)
                .HasForeignKey(p => p.CollectorEntryId);

            modelBuilder.Entity<PaymentDetail>()
            .HasOne(p => p.PlanDetail)
            .WithMany(d => d.Payments)
            .HasForeignKey(p => p.PlanDetailId);
        }


    }


    
}
