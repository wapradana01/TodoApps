using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.DataAccess.Bases;
using Todo.Api.DataAccess.Models;

namespace Todo.Api.DataAccess.Builders
{
    public class UserEntityBuilder : EntityBaseBuilder<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.UserName)
                .HasMaxLength(100);

            builder
                .Property(e => e.FullName)
                .HasMaxLength(150);

            builder
                .Property(e => e.Password)
                .HasMaxLength(500);
        }
    }
}
