using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Models.UserView
{
    public class CurrentUserInfoViewModel
    {
        public string[] Access { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; } 

        /// <summary>
        /// 登录用户显示名称
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 可访问的页面
        /// </summary>
        public List<string> Pages { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public Dictionary<string,List<string>> Permissions { get; set; }
    }
}
