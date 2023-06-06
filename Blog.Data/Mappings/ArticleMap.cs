using Blog.Core.Entities;
using Blog.Entitiy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article
            {
                Id = Guid.NewGuid(),
                Title = "Asp.net Core Blog",
                Content = "Blog Sayfamıza hoşgeldiniz.",
                ViewCount = 15,
                CategoryId = Guid.Parse("ABC8D06F-0A4B-4724-BA66-BF978E15B82D"),
                ImageId = Guid.Parse("ABC8D06F-0A4B-4724-BA66-BF978E15B82D"),
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = Guid.Parse("{AAAC3B44-AB15-4970-A3EA-A20C906A9DBF}")
            },
            new Article
            {
                Id = Guid.NewGuid(),
                Title = "Asp.net Core Blog2",
                Content = "Blog Sayfamıza hoşgeldiniz.2",
                ViewCount = 15,
                CategoryId = Guid.Parse("{0EB98344-6BA2-40B7-A21F-C774E718CD96}"),
                ImageId = Guid.Parse("3509133C-5A93-4C02-9A55-AD7E02EAA1FD"),
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = Guid.Parse("{0FC132E2-5E2A-4B8D-8265-5AF49E96AFF9}")

            });

        }
    }
}




