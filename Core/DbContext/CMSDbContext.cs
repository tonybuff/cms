using System;
using System.Collections.Generic;
using System.Text;
using Core.Mapping;
using Core.Models;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
