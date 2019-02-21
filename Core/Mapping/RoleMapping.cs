using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasKey(f => f.Id);
            //配置一对多
            builder.HasMany(f => f.Users).WithOne(f=>f.UserRoles).HasForeignKey(f=>f.RoleId).OnDelete(DeleteBehavior.Cascade);
            //配置一对一
            builder.HasOne(f => f.CreateUser).WithOne().HasForeignKey<Roles>(f => f.CreatedByUserGuid).HasConstraintName("FK_Roles_CreateUsers");
            //配置一对一
            builder.HasOne(f => f.ModifiedUser).WithOne().HasForeignKey<Roles>(f => f.ModifiedByUserGuid).HasConstraintName("FK_Roles_ModifyUser");
        }
    }
}
