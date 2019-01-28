using System;
using System.Collections.Generic;
using System.Text;
using Core.DbContext;
using Core.Models;

namespace Core.Repository.Imp
{
    public class UserRepository:BaseRepository<Users,Guid>,IUserRepository
    {
        public UserRepository(CMSDbContext dbContext) :base(dbContext)
        { 
           
        }

        
    }
}
