using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class RAM
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int RAMSize { get; set; }
        public int UnitPrice { get; set; }
        public int SupplierId { get; set; }
        public HardwareSuppliers? HardwareSupplier { get; set; }
    }

    public static class RAMEntityConfiguration
    {

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RAM>()
                .HasOne(ram => ram.HardwareSupplier)
                .WithMany()
                .HasForeignKey(ram => ram.SupplierId);

        }
    }
}
