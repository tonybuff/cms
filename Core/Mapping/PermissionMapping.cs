using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mapping
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Menus).WithMany(f => f.Permissions).HasForeignKey(f => f.MenuId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
