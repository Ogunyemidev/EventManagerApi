using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApi.Domain;

namespace MyApi.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class UserEntityTypeConfigurations
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.HashedPassword)
                .HasColumnName("hashed_password")
                .HasColumnType("varchar(255)")
                .IsRequired();

                
            builder.Property(u => u.Role)
                .HasColumnName("role")
                .HasColumnType("varchar(50)")
                .HasConversion<EnumToStringConverter<Role>>()
                .IsRequired();

               

            builder.Property(i => i.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(i => i.UpdatedDate)
                .HasColumnName("updated_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(i => i.IsDeleted)
                .HasColumnName("is_deleted")
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(i => i.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnType("varchar(100)")
                .IsRequired();
            
            builder.Property(i => i.UpdatedBy)
                .HasColumnName("updated_by")
                .HasColumnType("varchar(100)");      
        }
        
    }
}