using Core.DbContext;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository.Imp
{
    public class RolesRepository : BaseRepository<Roles, Guid>, IRolesRepository
    {
        public RolesRepository(CMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
