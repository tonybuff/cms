using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Models.RoleView
{
    public class RoleEditView
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
        /// <summary>
        /// 是否是超级管理员(超级管理员拥有系统的所有权限)
        /// </summary>
        public bool IsSuperAdministrator { get; set; }
        /// <summary>
        /// 是否是系统内置角色(系统内置角色不允许删除,修改操作)
        /// </summary>
        public bool IsBuiltin { get; set; }

        public Guid CreateBy { get; set; }

        public Guid ModifyBy { get; set; }
    }
}
