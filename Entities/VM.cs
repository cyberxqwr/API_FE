using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class VM
    {
        public int Id { get; set; }

        public int VDCId { get; set; }
        public VDC VDC { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string OSName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string AllocatedIP { get; set; }
        public int AllocatedRAM { get; set; }
        public int AllocatedVCPU { get; set; }
        public int AllocatedStorage { get; set; }

    }

    public static class VMEntityConfiguration
    {

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VM>()
                .HasOne(vm => vm.VDC)
                .WithMany(vm => vm.VMs)
                .HasForeignKey(vm => vm.VDCId);
        }
    }
}
