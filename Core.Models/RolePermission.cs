using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// 角色权限关系表
    /// </summary>
    public class RolePermission:BaseEntity<Guid>
    {
        /// <summary>
        /// 角色编码
        /// </summary>
        [Required]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public virtual Roles Role { get; set; }

        /// <summary>
        /// 权限实体
        /// </summary>
        public virtual Permission Permission { get; set; }

    }
}
