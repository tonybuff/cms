using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Common.Enums;

namespace Core.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class Users:BaseEntity<Guid>
    {
        [Required]
        [Column(TypeName = "nvarchar(50)", Order = 10)]
        public string LoginName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string DisplayName { get; set; }


        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }


        [Column(TypeName = "nvarchar(255)", Order = 100)]
        public string Avatar { get; set; }

        public int IsLocked { get; set; }

        //[EnumDataType(typeof(UserStatus))]
        public UserStateEnums Status { get; set; }

        public int IsDeleted { get; set; }
        
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
        /// 用户描述信息
        /// </summary>
        [Column(TypeName = "nvarchar(800)")]
        public string Description { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 用户的角色集合
        /// </summary>
        public virtual Roles UserRoles { get; set; }
    }
}
