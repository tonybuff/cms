using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Models.RoleView
{
    public class RoleRequestModel:RequestPayload
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public IsDeleted IsDeleted { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
    }
}
