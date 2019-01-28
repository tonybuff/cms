using System;
using System.Collections.Generic;
using System.Text;

using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Mapping
{
    public class MenuMapping : IEntityTypeConfiguration<Menus>
    {
        public void Configure(EntityTypeBuilder<Menus> builder)
        {
            builder.HasKey(f => f.Id);
        }
    }
}
