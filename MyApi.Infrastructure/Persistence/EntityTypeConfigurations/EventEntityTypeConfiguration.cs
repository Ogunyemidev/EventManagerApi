
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApi.Domain.Entities;

namespace MyApi.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class EventEntityTypeConfiguration
        : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            // Primary Key
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();

            // Base Entity
            builder.Property(e => e.CreatedDate)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.UpdatedDate)
                .IsRequired(false);

            builder.Property(e => e.UpdatedBy)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.IsDeleted)
                .IsRequired();

            // Organizer
            builder.Property(e => e.OrganizerId)
                .IsRequired();

            builder.HasOne(e => e.Organizer)
                .WithMany()
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event Name
            builder.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(200);

            // Event Description
            builder.Property(e => e.EventDescription)
                .IsRequired(false)
                .HasMaxLength(2000);

            // Venue
            builder.Property(e => e.Eventvenue)
                .IsRequired()
                .HasMaxLength(500);

            // Event Date
            builder.Property(e => e.EventDate)
                .IsRequired();

            // Event Status
            builder.Property(e => e.EventStatus)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            // Event -> TicketTypes
            builder.HasMany(e => e.TicketTypes)
                .WithOne()
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(e => e.OrganizerId);

            builder.HasIndex(e => e.EventDate);

            builder.HasIndex(e => e.EventStatus);

            builder.HasIndex(e => e.EventName);

            builder.HasIndex(e => e.IsDeleted);
        }
    }
}
