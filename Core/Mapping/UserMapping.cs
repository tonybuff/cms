using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Role).WithMany(f=>f.UserList).HasForeignKey(f=>f.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
