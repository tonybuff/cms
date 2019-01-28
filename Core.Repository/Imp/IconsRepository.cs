using Core.DbContext;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository.Imp
{
    public class IconsRepository : BaseRepository<Icons, Guid>, IIconsRepository
    {
        public IconsRepository(CMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
