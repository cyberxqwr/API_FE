using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class AvailableIPs
    {

        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string Status { get; set; }

        public int VLANId { get; set; }
        public VLAN VLAN { get; set; }

        public static class AvailableIPsEntityConfiguration
        {
            public static void Configure(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<AvailableIPs>()
                    .HasOne(ip => ip.VLAN)
                    .WithMany(ip => ip.AvailableIPs)
                    .HasForeignKey(vm => vm.VLANId);
            }
        }
    }


    }
 
