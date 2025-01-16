using Microsoft.EntityFrameworkCore;
using Paslauga.Helpers;

namespace Paslauga.Entities
{
    public class ProviderResources
    {
        public int Id { get; set; }
        public Provider? Provider { get; set; }
        public int ProviderId { get; set; }
        public HardwareType HardwareType { get; set; }
        public CPU? CPU { get; set; }
        public RAM? RAM { get; set; }
        public Storage? Storage { get; set; }
        public int HardwareId { get; set; }

        public static class ProviderResourcesEntityConfiguration
        {

            public static void Configure(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ProviderResources>()
                    .HasOne(pr => pr.Provider)
                    .WithMany()
                    .HasForeignKey(pr => pr.ProviderId);

                modelBuilder.Entity<ProviderResources>()
                    .HasOne(pr => pr.CPU)
                    .WithMany()
                    .HasForeignKey(pr => pr.HardwareId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<ProviderResources>()
                    .HasOne(pr => pr.RAM)
                    .WithMany()
                    .HasForeignKey(pr => pr.HardwareId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<ProviderResources>()
                    .HasOne(pr => pr.Storage)
                    .WithMany()
                    .HasForeignKey(pr => pr.HardwareId)
                    .OnDelete(DeleteBehavior.Restrict);


                modelBuilder.Entity<ProviderResources>()
                    .Property(pr => pr.HardwareType)
                    .HasConversion<int>();
            }

        }

    }
}