using Core.DbContext;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository.Imp
{
    public class PermissionRepository : BaseRepository<Permission, Guid>, IPermissionRepository
    {
        public PermissionRepository(CMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
