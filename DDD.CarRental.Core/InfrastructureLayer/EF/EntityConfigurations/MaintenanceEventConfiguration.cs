using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class MaintenanceEventConfiguration : IEntityTypeConfiguration<MaintenanceEvent>
    {
        public void Configure(EntityTypeBuilder<MaintenanceEvent> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();
            builder.Ignore(m => m.DomainEvents);
            builder.Ignore(m => m.Parts); // ❗ TO JEST KLUCZOWE

            builder.Property(m => m.CarId).IsRequired();
            builder.Property(m => m.Date).IsRequired();
            builder.Property(m => m.Description).IsRequired().HasMaxLength(500);

            builder.OwnsOne(m => m.Cost, cb =>
            {
                cb.Property(c => c.Value).HasColumnName("Cost_Value");
                cb.Property(c => c.Currency).HasColumnName("Cost_Currency");
            });

            builder.OwnsMany<PartReplaced>("_parts", part =>
            {
                part.WithOwner().HasForeignKey("MaintenanceEventId");
                part.ToTable("PartsReplaced");

                part.Property<int>("Id");
                part.HasKey("Id");

                part.Property(p => p.Name).IsRequired();
                part.Property(p => p.Manufacturer).IsRequired();

                part.OwnsOne(p => p.Cost, cb =>
                {
                    cb.Property(c => c.Value).HasColumnName("PartCost_Value");
                    cb.Property(c => c.Currency).HasColumnName("PartCost_Currency");
                });
            });
        }

    }
}
