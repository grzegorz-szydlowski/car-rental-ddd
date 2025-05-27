using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> rentalConfiguration)
        {
            rentalConfiguration.HasKey(r => r.Id);
            rentalConfiguration.Property(r => r.Id).ValueGeneratedNever();
            rentalConfiguration.Ignore(r => r.DomainEvents);

            rentalConfiguration.Property(r => r.Started).IsRequired();
            rentalConfiguration.Property(r => r.Finished);
            rentalConfiguration.Property(r => r.CarId).IsRequired();
            rentalConfiguration.Property(r => r.DriverId).IsRequired();

            rentalConfiguration.OwnsOne(r => r.Total, rb =>
            {
                rb.Property(m => m.Value).HasColumnName("Total_Value");
                rb.Property(m => m.Currency).HasColumnName("Total_Currency");
            });
        }
    }
}
