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
            builder.HasMany(f => f.UserList).WithOne(f=>f.Role).HasForeignKey(f=>f.RoleId).OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
