using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class VLAN
    {
        public int Id { get; set; }

        public int? NetworkPoolId { get; set; } = 0;
        public NetworkPool? NetworkPool { get; set; }
        public string Name { get; set; }
        public string GatewayCIDR { get; set; }
        public string StaticIPBegin { get; set; }
        public string StaticIPEnd { get; set; }
        public ICollection<AvailableIPs> AvailableIPs { get; set; } = new List<AvailableIPs>();

        public static class VLANEntityConfiguration
        {
            public static void Configure(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<VLAN>()
                    .HasOne(np => np.NetworkPool)
                    .WithMany(np => np.VLANs)
                    .HasForeignKey(np => np.NetworkPoolId);

                modelBuilder.Entity<VLAN>()
                    .Property(np => np.NetworkPoolId)
                    .HasDefaultValue(0);
            }
        }

    }
}