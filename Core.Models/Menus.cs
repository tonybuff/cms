﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// 菜单实体类
    /// </summary>
    public class Menus:BaseEntity<Guid>
    {
        public Menus()
        {
            Permissions = new HashSet<Permission>();
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [Column(TypeName = "nvarchar(255)")]
        public string Url { get; set; }
        /// <summary>
        /// 页面别名
        /// </summary>
        [Column(TypeName = "nvarchar(255)")]
        public string Alias { get; set; }
        /// <summary>
        /// 菜单图标(可选)
        /// </summary>
        [Column(TypeName = "nvarchar(128)")]
        public string Icon { get; set; }
        /// <summary>
        /// 父级GUID
        /// </summary>
        public Guid? ParentGuid { get; set; }
        /// <summary>
        /// 上级菜单名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 菜单层级深度
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [Column(TypeName = "nvarchar(800)")]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否可用(0:禁用,1:可用)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 是否已删
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 是否为默认路由
        /// </summary>
        public int IsDefaultRouter { get; set; }

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
        /// 菜单拥有的权限列表
        /// </summary>
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
