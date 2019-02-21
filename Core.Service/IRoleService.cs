using System;
using System.Collections.Generic;
using System.Text;
using Core.Service.Models;
using Core.Service.Models.RoleView;

namespace Core.Service
{
    public interface IRoleService
    {

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        ResultDataModel GetAllRoles();

        /// <summary>
        /// 分页查询角色列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultDataModel GetRoleList(RoleRequestModel model);

        /// <summary>
        /// 根据编号获取角色详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultDataModel GetRoleDetailById(Guid id);

        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultDataModel SaveEdit(RoleEditView model);
    }
}
