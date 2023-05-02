using Blog.Entitiy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder) 
        {
            builder.HasData(new Category
            {
                Id = Guid.Parse("ABC8D06F-0A4B-4724-BA66-BF978E15B82D"),
                Name = "ASP.NET CORE",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Category
            {
                 Id = Guid.Parse("{0EB98344-6BA2-40B7-A21F-C774E718CD96}"),
                 Name = "ASP.NET CORE2",
                 CreatedBy = "Admin Test",
                 CreatedDate = DateTime.Now,
                 IsDeleted = false
            });


        }
    }
}
