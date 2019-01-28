using System;
using System.Collections.Generic;
using System.Text;
using Core.DbContext;
using Core.Models;

namespace Core.Repository.Imp
{
    public class MenuRepository : BaseRepository<Menus, Guid>, IMenuRepository
    {
        public MenuRepository(CMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
