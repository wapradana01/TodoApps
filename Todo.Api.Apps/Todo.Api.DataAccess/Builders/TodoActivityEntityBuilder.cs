using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.DataAccess.Bases;
using Todo.Api.DataAccess.Models;

namespace Todo.Api.DataAccess.Builders
{
    public class TodoActivityEntityBuilder : EntityBaseBuilder<TodoActivity>
    {
        public override void Configure(EntityTypeBuilder<TodoActivity> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.ActivityNo)
                .HasMaxLength(100);

            builder
                .Property(e => e.ActivityName)
                .HasMaxLength(500);

            builder
                .Property(e => e.ActivityStatus)
                .HasConversion<string>()
                .HasMaxLength(200);
        }
    }
}
