using System;
using System.Collections.Generic;
using System.Text;
using Core.Service.Models;
using Core.Service.Models.IconsViewModel;

namespace Core.Service
{
    public interface IIConsService
    {
        /// <summary>
        /// 分页获取图标列表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultDataModel GetList(IconsRequestModel model);

        /// <summary>
        /// 根据关键字查询图标
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        ResultDataModel FindIconByKey(string keywords);
    }
}
