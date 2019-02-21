using System;
using System.Collections;
using System.Collections.Generic;
using Core.Service.Models;
using Core.Service.Models.UserView;
using System.Linq.Expressions;
using Core.Common.Enums;

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
        /// 根据条件分页获取数据
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        ResultDataModel GetPageList(UserRequestModel userRequest);

        /// <summary>
        /// 获取当前登录用户信息包括权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ResultDataModel GetCurrentUserProfile(Guid userId);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultDataModel CreateUser(UserEditViewModel model);

        /// <summary>
        /// 根据主键查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultDataModel GetUserById(Guid id);

        /// <summary>
        /// 保存编辑后的用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultDataModel UpdateUser(UserEditViewModel model);

        /// <summary>
        /// 删除或者恢复当前用户
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        ResultDataModel UpdateIsDelete(IsDeleted isDeleted, string ids);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userStateEnums"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        ResultDataModel UpdateStatus(UserStateEnums userStateEnums, string ids);
    }
}
