using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Service.Models.UserView
{
    public class UserEditViewModel
    {

        public Guid? Guid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="登录名称不能为空")]
        public string LoginName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="显示名称不能为空")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserType UserType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsLocked IsLocked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserStateEnums Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsDeleted IsDeleted { get; set; }
        /// <summary>
        /// 用户描述信息
        /// </summary>
        public string Description { get; set; }

        public Guid? RoleId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateBy { get; set; }
        
        /// <summary>
        /// 修改人
        /// </summary>
        public Guid ModifyBy { get; set; }
    }

}
