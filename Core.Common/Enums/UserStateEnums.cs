using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Enums
{
    /// <summary>
    /// 用户账号状态
    /// </summary>
    public enum UserStateEnums
    {
        /// <summary>
        /// 未指定
        /// </summary>
        All = -1,
        /// <summary>
        /// 已禁用
        /// </summary>
        Forbidden = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1
    }
}
