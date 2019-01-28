using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository
{
    public interface IUserRepository: IBaseRepository<Users,Guid>
    {
    }
}
