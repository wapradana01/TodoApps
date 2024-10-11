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
    public class DocNumberConfigEntityBuilder : EntityBaseBuilder<DocNumberConfig>
    {
        public override void Configure(EntityTypeBuilder<DocNumberConfig> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.TransactionTypeCode)
                .HasMaxLength(15);

            builder
                .Property(e => e.Description)
                .HasMaxLength(500);
        }
    }
}
