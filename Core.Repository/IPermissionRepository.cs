﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;

namespace Core.Repository
{
    public interface IPermissionRepository:IBaseRepository<Permission,Guid>
    {
    }
}
