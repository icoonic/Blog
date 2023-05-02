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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId = Guid.Parse("36E8F3D6-0E25-4D65-8CA6-81CA01C49461"),
                RoleId = Guid.Parse("EA39BC82-671F-42FE-A370-B92ABD43C5AC")
            },

            new AppUserRole
            {
                UserId = Guid.Parse("C5D057F3-9607-4F33-AF58-10BFAD7F664C"),
                RoleId = Guid.Parse("2420F0B7-CA5F-4721-8BCB-3C334928DC8D")

            });

        }
    }
}
