using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Models.UserView
{
    public class UserRequestModel:RequestPayload
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public IsDeleted IsDeleted { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStateEnums Status { get; set; }
    }
}
