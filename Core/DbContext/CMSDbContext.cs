using System;
using System.Collections.Generic;
using System.Text;
using Core.Mapping;
using Core.Models;
using Core.Service.Models.UserView;
using Microsoft.EntityFrameworkCore;

namespace Core.DbContext
{
    public class CMSDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public CMSDbContext(DbContextOptions<CMSDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Icons> Icons { get; set; }

        public DbQuery<PermissionWithMenu> permissionWithMenus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new RolePermissionMapping());
            modelBuilder.ApplyConfiguration(new PermissionMapping());
            modelBuilder.ApplyConfiguration(new MenuMapping());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
