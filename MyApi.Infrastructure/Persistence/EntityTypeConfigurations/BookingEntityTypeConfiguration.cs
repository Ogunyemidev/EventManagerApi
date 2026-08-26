
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
            // =========================================================
            // Table
            // =========================================================

            builder.ToTable("Bookings");


            // =========================================================
            // Primary Key
            // Booking inherits Id from BaseEntity
            // =========================================================

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .IsRequired();


            // =========================================================
            // BaseEntity Properties
            // =========================================================

            builder.Property(b => b.CreatedDate)
                .IsRequired();

            builder.Property(b => b.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.UpdatedDate)
                .IsRequired(false);

            builder.Property(b => b.UpdatedBy)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(b => b.IsDeleted)
                .IsRequired();


            // =========================================================
            // Customer
            // =========================================================

            builder.Property(b => b.CustomerId)
                .IsRequired();

            builder.HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================================================
            // Event
            // =========================================================

            builder.Property(b => b.EventId)
                .IsRequired();

            builder.HasOne(b => b.Event)
                .WithMany()
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================================================
            // Booking Status
            // =========================================================

            builder.Property(b => b.BookingStatus)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);


            // =========================================================
            // Reserved At
            // =========================================================

            builder.Property(b => b.ReservedAt)
                .IsRequired();


            // =========================================================
            // Expired At
            // =========================================================

            builder.Property(b => b.ExpiredAt)
                .IsRequired();


            // =========================================================
            // Total Amount
            // =========================================================

            builder.Property(b => b.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);


            // =========================================================
            // Booking -> Payments
            // =========================================================

            builder.HasMany(b => b.Payments)
                .WithOne(p => p.Booking)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================================================
            // Indexes
            // =========================================================

            builder.HasIndex(b => b.CustomerId);

            builder.HasIndex(b => b.EventId);

            builder.HasIndex(b => b.BookingStatus);

            builder.HasIndex(b => b.ReservedAt);

            builder.HasIndex(b => b.IsDeleted);
        }
    }
}
