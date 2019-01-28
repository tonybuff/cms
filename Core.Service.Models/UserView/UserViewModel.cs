using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Models.UserView
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public string Password { get; set; }

        public int Status { get; set; }

        public int IsLocked { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdministrator { get; set; }
    }
}
