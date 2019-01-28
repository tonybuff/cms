using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Models.MenuView
{
    public class MenuRequestModel:RequestPayload
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public IsDeleted IsDeleted { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// 上级菜单GUID
        /// </summary>
        public Guid? ParentGuid { get; set; }
    }
}
