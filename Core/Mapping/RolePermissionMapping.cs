using System;
using System.Collections.Generic;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text;

namespace Core.Mapping
{
    public class RolePermissionMapping : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Role).WithMany(f => f.RolePermissions).HasForeignKey(f => f.RoleId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Permission)
                   .WithMany(x => x.RolesPermission)
                   .HasForeignKey(x => x.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
