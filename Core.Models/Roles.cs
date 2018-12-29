using System;
using System.Collections.Generic;
using System.Text; 

namespace Core.Models
{
    public class Roles:BaseEntity<Guid>
    {
        public Roles()
        {
            UserList = new HashSet<Users>();
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual ICollection<Users> UserList { get; set; }
    }
}
