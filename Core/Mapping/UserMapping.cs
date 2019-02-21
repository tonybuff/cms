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
            builder.HasOne(f => f.UserRoles).WithMany(f=>f.Users).HasForeignKey(f=>f.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(f => f.CreateUser).WithOne().HasForeignKey<Users>(f => f.CreatedByUserGuid).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(f => f.ModifiedUser).WithOne().HasForeignKey<Users>(f => f.ModifiedByUserGuid).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
