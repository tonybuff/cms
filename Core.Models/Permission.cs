using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
   public class Permission:BaseEntity<Guid>
    {
        public Permission()
        {
            RolesPermission = new HashSet<RolePermission>();
        }

        /// <summary>
        /// 菜单GUID
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        /// <summary>
        /// 权限操作码
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(80)")]
        public string ActionCode { get; set; }

        /// <summary>
        /// 图标(可选)
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 是否已删
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 权限类型(0:菜单,1:按钮/操作/功能等)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public Guid CreatedByUserGuid { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// 最近修改者ID
        /// </summary>
        public Guid? ModifiedByUserGuid { get; set; }
        /// <summary>
        /// 最近修改者姓名
        /// </summary>
        public string ModifiedByUserName { get; set; }

        /// <summary>
        /// 关联的菜单
        /// </summary>
        public virtual Menus Menus { get; set; }
        /// <summary>
        /// 权限所属的角色集合
        /// </summary>
        public virtual ICollection<RolePermission> RolesPermission { get; set; }
    }
}
