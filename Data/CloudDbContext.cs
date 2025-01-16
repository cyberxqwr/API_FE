using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Paslauga.Entities;
using static Paslauga.Entities.AvailableIPs;
using static Paslauga.Entities.ProviderResources;
using static Paslauga.Entities.VDC;
using static Paslauga.Entities.VLAN;

namespace Paslauga.Data
{
    public class CloudDbContext : IdentityDbContext
    {

        public CloudDbContext(DbContextOptions<CloudDbContext> options) : base(options) { }

        public DbSet<Cloud> Cloud { get; set; }
        public DbSet<VDC> VDC { get; set; }
        public DbSet<NetworkPool> NetworkPool { get; set; }
        public DbSet<VLAN> VLAN { get; set; }
        public DbSet<VM> VM { get; set; }
        public DbSet<AvailableIPs> AvailableIPs { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<CPU> CPU { get; set; }
        public DbSet<RAM> RAM { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<HardwareSuppliers> HardwareSuppliers { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<ProviderResources> ProviderResources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=cloud.db");
            }

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            VDCEntityConfiguration.Configure(modelBuilder);
            VLANEntityConfiguration.Configure(modelBuilder);
            AvailableIPsEntityConfiguration.Configure(modelBuilder);
            VMEntityConfiguration.Configure(modelBuilder);
            RAMEntityConfiguration.Configure(modelBuilder);
            CPUEntityConfiguration.Configure(modelBuilder);
            StorageEntityConfiguration.Configure(modelBuilder);
            ProviderResourcesEntityConfiguration.Configure(modelBuilder);
        }
    }
}
