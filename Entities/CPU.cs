using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class CPU
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Cores { get; set; }
        public int HyperThreading { get; set; }
        public float GHz { get; set; }
        public int UnitPrice { get; set; }
        public int SupplierId { get; set; }
        public HardwareSuppliers? HardwareSupplier { get; set; }
    }

    public static class CPUEntityConfiguration
    {

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CPU>()
                .HasOne(cpu => cpu.HardwareSupplier)
                .WithMany()
                .HasForeignKey(cpu => cpu.SupplierId);

        }
    }
}
 
