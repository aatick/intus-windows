using IntusWindows.DAL.DataModels;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.DAL.ApplicationDbContext
{
    public class IntusWindowsDbContext : DbContext
    {
        public IntusWindowsDbContext(DbContextOptions<IntusWindowsDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Window> Windows { get; set; }
        public virtual DbSet<SubElement> SubElements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>().HasKey(e => e.Id);
            //modelBuilder.Entity<Window>().HasKey(e => e.Id);
            //modelBuilder.Entity<SubElement>().HasKey(e => e.Id);
            
            //modelBuilder.Entity<Window>()
            //    .HasOne(p => p.Order)
            //    .WithMany(b => b.Windows)
            //    .HasForeignKey(p => p.OrderId);

            //modelBuilder.Entity<SubElement>()
            //    .HasOne(p => p.Window)
            //    .WithMany(b => b.SubElements)
            //    .HasForeignKey(p => p.WindowId);
        }
    }
}
