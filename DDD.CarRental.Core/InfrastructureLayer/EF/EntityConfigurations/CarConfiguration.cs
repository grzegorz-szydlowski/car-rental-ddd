using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Ignore(c => c.DomainEvents);

            builder.Property(c => c.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.HasOne(typeof(Distance), "CurrentDistance")
                .WithMany()
                .HasForeignKey("CurrentDistance_CarId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(typeof(Distance), "TotalDistance")
                .WithMany()
                .HasForeignKey("TotalDistance_CarId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(c => c.CurrentPosition, cb =>
            {
                cb.Property(p => p.X).HasColumnName("CurrentPosition_X");
                cb.Property(p => p.Y).HasColumnName("CurrentPosition_Y");
                cb.Property(p => p.Unit).HasColumnName("CurrentPosition_Unit");
            });
        }
    }
}
