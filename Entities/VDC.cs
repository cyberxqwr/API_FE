using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class VDC
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Organisation? Organisation { get; set; }
        public int? OrganisationId { get; set; }
        public int? VCPUMax { get; set; }
        public int? VCPUAllocated { get; set; }
        public int? VMemoryMax { get; set; }
        public int? VMemoryAllocated { get; set; }
        public int? VStorageMax { get; set; }
        public int? VStorageUsed { get; set; }

        public int? NetworkPoolId { get; set; }
        public NetworkPool? NetworkPool { get; set; } = null;

        public ICollection<VM> VMs { get; set; } = new List<VM>();

        public static class VDCEntityConfiguration
        {
            public static void Configure(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<VDC>()
                    .HasOne(np => np.NetworkPool)
                    .WithMany()
                    .HasForeignKey(np => np.NetworkPoolId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<VDC>()
                    .HasOne(o => o.Organisation)
                    .WithMany()
                    .HasForeignKey(o => o.OrganisationId);
                #region "Set Default Vals"

                modelBuilder.Entity<VDC>()
                    .Property(o => o.VCPUAllocated)
                    .HasDefaultValue(0);

                modelBuilder.Entity<VDC>()
                    .Property(o => o.VCPUMax)
                    .HasDefaultValue(0);

                modelBuilder.Entity<VDC>()
                    .Property(o => o.VMemoryAllocated)
                    .HasDefaultValue(0);

                modelBuilder.Entity<VDC>()
                    .Property(o => o.VMemoryMax)
                    .HasDefaultValue(0);

                modelBuilder.Entity<VDC>()
                    .Property(o => o.VStorageMax)
                    .HasDefaultValue(0);

                modelBuilder.Entity<VDC>()
                    .Property(o => o.VStorageUsed)
                    .HasDefaultValue(0);

                modelBuilder.Entity<VDC>()
                    .Property(o => o.OrganisationId)
                    .HasDefaultValue(0);

                modelBuilder.Entity<VDC>()
                    .Property(np => np.NetworkPoolId)
                    .HasDefaultValue(null);
                #endregion
            }
        }

    }

}
 
