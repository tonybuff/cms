using System;
using System.Collections.Generic;
using System.Text;
using Core.DbContext;
using Core.Models;

namespace Core.Repository.Imp
{
    public class RolePermissionRepository : BaseRepository<RolePermission, Guid>, IRolePermissionRepository
    {
        public RolePermissionRepository(CMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
