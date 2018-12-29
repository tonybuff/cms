using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class Users:BaseEntity<Guid>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        public Guid? RoleId { get; set; }

        /// <summary>
        /// 当前角色
        /// </summary>
        public virtual Roles Role { get; set; }
    }
}
