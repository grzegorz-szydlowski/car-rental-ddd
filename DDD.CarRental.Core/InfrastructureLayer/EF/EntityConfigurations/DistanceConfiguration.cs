using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class DistanceConfiguration : IEntityTypeConfiguration<Distance>
    {
        public void Configure(EntityTypeBuilder<Distance> builder)
        {
            builder.Property<long>("Id").IsRequired();
            builder.HasKey("Id"); // Shadow primary key if not defined

            builder.Property(d => d.Value)
                .IsRequired();

            builder.Property(d => d.Unit)
                .IsRequired()
                .HasMaxLength(10);

            // You can define a discriminator if needed or use table-per-hierarchy strategies

            // Optional: if you want to expose shadow foreign key property names
            builder.Property<long?>("CarId"); // shadow FK
        }
    }
}