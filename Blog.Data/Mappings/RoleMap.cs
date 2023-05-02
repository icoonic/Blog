using Blog.Entitiy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            builder.HasData(new AppRole
            {
                Id = Guid.Parse("EA39BC82-671F-42FE-A370-B92ABD43C5AC"),
                Name = "Superadmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString() // iki kişi aynı anda sistem üzerinde değişiklik yapacak olursa çakışmasını engelleyen yapı
            },
             new AppRole
             {
                 Id = Guid.Parse("2420F0B7-CA5F-4721-8BCB-3C334928DC8D"),
                 Name = "Admin",
                 NormalizedName = "ADMIN",
                 ConcurrencyStamp = Guid.NewGuid().ToString()
             },
             new AppRole
             {
                 Id = Guid.Parse("0C2D5F25-251C-468D-8BCB-8AF1EE12A89D"),
                 Name = "User",
                 NormalizedName = "USER",
                 ConcurrencyStamp = Guid.NewGuid().ToString()

             });

        }
    }
}
