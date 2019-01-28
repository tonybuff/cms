using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Models.IconsViewModel
{
    /// <summary>
    /// 图标请求参数实体
    /// </summary>
    public class IconsRequestModel:RequestPayload
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
