using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApi.Domain.Entities;

namespace MyApi.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class BookingEntityTypeConfiguration 
        : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Table
            builder.ToTable("Bookings");

            // Primary Key
            builder.HasKey(b => b.BookingId);

            // CustomerId
            builder.Property(b => b.CustomerId)
                   .IsRequired();

            // EventId
            builder.Property(b => b.EventId)
                   .IsRequired();

            // Booking Status
            builder.Property(b => b.BookingStatus)
                   .IsRequired()
                   .HasConversion<string>();

            // ReservedAt
            builder.Property(b => b.ReservedAt)
                   .IsRequired();

       
            // TotalAmount
            builder.Property(b => b.TotalAmount)
                   .IsRequired()
                   .HasPrecision(10, 2);


            // Event relationship
            builder.HasOne(b => b.Event)
                   .WithMany()
                   .HasForeignKey(b => b.EventId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(b => b.CustomerId);

            builder.HasIndex(b => b.EventId);

            builder.HasIndex(b => b.BookingStatus);
        }
    }
}