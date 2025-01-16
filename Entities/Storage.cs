using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class Storage
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int StorageSize { get; set; }
        public int UnitPrice { get; set; }
        public int SupplierId { get; set; }
        public HardwareSuppliers? HardwareSupplier { get; set; }
    }

    public static class StorageEntityConfiguration
    {

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>()
                .HasOne(st => st.HardwareSupplier)
                .WithMany()
                .HasForeignKey(st => st.SupplierId);

        }
    }
}
