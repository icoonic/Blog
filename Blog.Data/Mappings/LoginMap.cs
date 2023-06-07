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
    public class LoginMap : IEntityTypeConfiguration<LoginUser>
    {
        public void Configure(EntityTypeBuilder<LoginUser> builder)
        {
            throw new NotImplementedException();
        }
    }
}
