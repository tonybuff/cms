using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.DbContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CMSDbContext>
    {
        public CMSDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CMSDbContext>();
            builder.UseSqlServer("server=.;uid=sa;pwd=123456;database=CMSDb");
            return new CMSDbContext(builder.Options);
        }
    }
}
