using System;
using System.Collections;
using System.Collections.Generic;
using Core.Service.Models;
using Core.Service.Models.UserView;

namespace Core.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        ResultDataModel Login(UserLoginModel loginModel);

        /// <summary>
        /// 分页获取用户数据
        /// </summary>
        /// <param name="total"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        ResultDataModel GetPageList(out int total, int page = 1, int size = 10);

        /// <summary>
        /// 获取当前登录用户信息包括权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ResultDataModel GetCurrentUserProfile(Guid userId);

        
    }
}
